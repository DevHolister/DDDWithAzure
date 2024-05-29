using Ardalis.Specification;
using Linde.Core.Coaching.Common.Interfaces.Services;
//using Linde.Domain.Coaching.CoachingActionPlanAggregate;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.Notifications;
using Linde.Domain.Coaching.PermissionAggregate;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.UserAggreagate;
using Linde.Domain.Coaching.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Linde.Persistence.Coaching;

public class CoachingDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IPublisher _publisher;

    public CoachingDbContext(DbContextOptions<CoachingDbContext> options)
        : base(options)
    {

    }

    public CoachingDbContext(
        DbContextOptions<CoachingDbContext> options,
        ICurrentUserService currentUserService,
        IPublisher publisher) : base(options)
    {
        _currentUserService = currentUserService;
        _publisher = publisher;
    }

    public DbSet<Country>? Country { get; set; }
    public DbSet<Permission>? Permission { get; set; }
    public DbSet<Role>? Role { get; set; }
    public DbSet<User>? User { get; set; }
    public DbSet<Plant> Plant { get; set; }
    public DbSet<UserPlant> Operator { get; set; }
    public DbSet<Menu>? Menu { get; set; }
    public DbSet<TblNotification>? Notification { get; set; }
    public DbSet<VwEmpleado>? vwEmployee { get; set; }
    public DbSet<CatQuestions>? CatQuestions { get; set; }
    public DbSet<QuestionsActivities>? QuestionsActivities { get; set; }
    public DbSet<VwPlantas>? vwPlantas { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = DateTime.Now;
                    entry.Entity.ModifiedBy = Guid.Parse(this._currentUserService.UserId);
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = Guid.Parse(this._currentUserService.UserId);
                    entry.Entity.ModifiedAt = DateTime.Now;
                    entry.Entity.ModifiedBy = Guid.Parse(this._currentUserService.UserId);
                    break;
                default:
                    break;
            }
        }

        var entitiesWithDomainEvents = ChangeTracker.Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = entitiesWithDomainEvents.SelectMany(x => x.DomainEvents).ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var item in domainEvents)
        {
            await _publisher.Publish(item, cancellationToken);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<VwEmpleado>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("huemplea_arc");

            entity.Property(e => e.NameComplete)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.NoEmPloyee)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NO_EMP");

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EMAIL");

            entity.Property(e => e.User)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("USUARIO");

            entity.Property(e => e.CIA)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CIA");

            entity.Property(e => e.Bu)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BU");

            entity.Property(e => e.NoAutorizationEmployee)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NO_EMP_AUTORIZA");

            entity.Property(e => e.CodeCountry)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Pais");

            entity.Property(e => e.NoSupervisor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NoEmpleadoJefe");

            entity.Property(e => e.Supervisor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Jefe");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NombreEmpleado");

            entity.Property(e => e.FirstLastName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ApellidoPaterno");

            entity.Property(e => e.SecondLastName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ApellidoMaterno");

        });

        builder.Entity<VwPlantas>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("JDELocalidades");

            entity.Property(e => e.NumSucursal)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NumSucursal");

            entity.Property(e => e.Sucursal)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NomSucursal");

            entity.Property(e => e.Pais)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Pais");

            entity.Property(e => e.Division)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Division");

            entity.Property(e => e.Estado)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Estado");

            entity.Property(e => e.Ciudad)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ResidenciaFiscal");

            entity.Property(e => e.Municipio)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Municipio");

        });

        builder.ApplyConfigurationsFromAssembly(typeof(CoachingDbContext).Assembly);
    }
}
