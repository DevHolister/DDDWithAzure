using Linde.Core.Coaching.Catalogs.Country.Errors;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Persistence.Coaching.Configurations
{
    internal class PlantAggregateConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            ConfigurePlantTable(builder);
           
        }

        private void ConfigurePlantTable(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("CAT_PLANT", Constants.Schema);
            builder.HasKey(x => x.PlantId);
            builder.Property(x => x.PlantId)
                .HasColumnName("PlantId")
                .ValueGeneratedNever();
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(150);
            builder.Property(x => x.Bu)
                .HasColumnName("Bu")
                .HasMaxLength(150);
            builder.Property(x => x.CountryId)
                .HasColumnName("CountryId")
                .ValueGeneratedNever();
            builder.Property(x => x.Division)
                .HasColumnName("Division")
                .HasMaxLength(150);
            builder.Property(x => x.State)
                .HasColumnName("State")
                .HasMaxLength(150);
            builder.Property(x => x.City)
                .HasColumnName("City")
                .HasMaxLength(150);
            builder.Property(x => x.Municipality)
                .HasColumnName("Municipality")
                .HasMaxLength(150);
            builder.Property(x => x.SuperintendentId)
                .HasColumnName("SuperintendentId")
                .ValueGeneratedNever();
            builder.Property(x => x.PlantManagerId)
                .HasColumnName("PlantManagerId")
                .ValueGeneratedNever();

            builder.Property(x => x.CountryId)
            .HasConversion(x => x.Value, value => CountryId.Create(value));

            builder.Property(x => x.SuperintendentId)
            .HasConversion(x => x.Value, value => UserId.Create(value));

            builder.Property(x => x.PlantManagerId)
            .HasConversion(x => x.Value, value => UserId.Create(value));

            builder.HasOne(x => x.Country)
            .WithMany(b => b.Plants)
            .HasForeignKey(b => b.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UserSuperIntendent)
            .WithMany(b => b.PlantSuperIntendet)
            .HasForeignKey(b => b.SuperintendentId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserManager)
            .WithMany(b => b.PlantManager)
            .HasForeignKey(b => b.PlantManagerId)
            .OnDelete(DeleteBehavior.NoAction);

        }

       
    }
}
