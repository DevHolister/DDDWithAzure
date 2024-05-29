using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations;

internal class RoleAggregateConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("CAT_ROLE", Constants.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("RoleId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => RoleId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsMany(x => x.PermissionItems, itemBuilder =>
        {
            itemBuilder.ToTable("ASOC_ROLE_PERMISSION", Constants.Schema);

            itemBuilder.HasKey("RoleId", "PermissionId");

            itemBuilder.Ignore(x => x.Visible)
                .Ignore(x => x.ModifiedAt)
                .Ignore(x => x.ModifiedBy)
                .Ignore(x => x.Id);
        });

        builder.Metadata.FindNavigation(nameof(Role.PermissionItems))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
