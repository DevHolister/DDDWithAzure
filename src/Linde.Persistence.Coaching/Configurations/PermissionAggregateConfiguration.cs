using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.PermissionAggregate;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations;

internal class PermissionAggregateConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("CAT_PERMISSION", Constants.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("PermissionId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => PermissionId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Actions)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Ignore(x => x.ModifiedBy)
            .Ignore(x => x.ModifiedAt)
            .Ignore(x => x.SpecifyCreatedBy)
            .Ignore(x => x.Visible);

        //builder.E.OwnsOne(t => t.Menu);
    }
}
