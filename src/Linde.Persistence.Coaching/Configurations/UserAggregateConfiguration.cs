using Linde.Domain.Coaching.UserAggreagate;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations;

internal class UserAggregateConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
        ConfigureUserRoleTable(builder);
        ConfigureUserCountryTable(builder);
    }

    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("CAT_USER", Constants.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("UserId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => UserId.Create(value));

        builder.HasIndex(x => x.UserName)
            .IsUnique();
    }

    private void ConfigureUserRoleTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(x => x.RoleItems, itemBuilder =>
        {
            itemBuilder.ToTable("ASOC_USER_ROLE", Constants.Schema);

            itemBuilder.HasKey("UserId", "RoleId");

            itemBuilder.Ignore(x => x.Visible)
                .Ignore(x => x.ModifiedAt)
                .Ignore(x => x.ModifiedBy)
                .Ignore(x => x.Id);

            itemBuilder.Property(x => x.CreatedBy)
                .IsRequired();

            itemBuilder.Property(x => x.CreatedAt)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(User.RoleItems))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureUserCountryTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(x => x.CountryItems, itemBuilder =>
        {
            itemBuilder.ToTable("ASOC_USER_COUNTRY", Constants.Schema);

            itemBuilder.HasKey("UserId", "CountryId");

            itemBuilder.Ignore(x => x.Visible)
                .Ignore(x => x.ModifiedAt)
                .Ignore(x => x.ModifiedBy)
                .Ignore(x => x.Id);

            itemBuilder.Property(x => x.CreatedBy)
                .IsRequired();

            itemBuilder.Property(x => x.CreatedAt)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(User.CountryItems))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
