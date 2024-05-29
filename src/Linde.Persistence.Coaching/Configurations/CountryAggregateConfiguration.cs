using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations;

internal class CountryAggregateConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("CAT_COUNTRY", Constants.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("CountryId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CountryId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.OriginalCode)
            .HasMaxLength(50)
            .IsRequired();
    }
}
