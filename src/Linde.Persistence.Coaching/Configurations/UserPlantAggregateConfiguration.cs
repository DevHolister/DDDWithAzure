 using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
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
    internal class UserPlantAggregateConfiguration : IEntityTypeConfiguration<UserPlant>
    {
        public void Configure(EntityTypeBuilder<UserPlant> builder)
        {          
            ConfigureRolePlantTable(builder);
        }

        private void ConfigureRolePlantTable(EntityTypeBuilder<UserPlant> builder)
        {
            builder.ToTable("ASOC_USERS_PLANTS", Constants.Schema);
            builder.HasKey(x => new { x.UserId, x.PlantId});

            builder.Ignore(x => x.Visible)
                .Ignore(x => x.SpecifyCreatedBy)
                .Ignore(x => x.ModifiedAt)
                .Ignore(x => x.ModifiedBy);            

            builder.HasOne(x => x.User)
           .WithMany(x => x.UserPlants)
           .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Plant)
           .WithMany(x => x.UserPlants)
           .HasForeignKey(x => x.PlantId);

        }
    }
}
