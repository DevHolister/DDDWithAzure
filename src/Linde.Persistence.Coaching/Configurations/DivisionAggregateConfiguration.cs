using Linde.Domain.Coaching.DivisionAggregate;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Linde.Persistence.Coaching.Configurations;

internal class DivisionAggregateConfiguration : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.ToTable("CAT_DIVISIONS", Constants.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("DivisionId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => DivisionId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}