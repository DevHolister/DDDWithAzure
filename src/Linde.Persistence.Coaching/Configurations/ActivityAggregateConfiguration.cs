using Linde.Domain.Coaching.ActivityAggregate;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.DivisionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Persistence.Coaching.Configurations
{
    internal class ActivityAggregateConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("CAT_ACTIVITIES", Constants.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ActivityId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => ActivityId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(400)
                .IsRequired();

            builder.HasMany(x => x.QuestionsActivities)
                 .WithOne(x => x.Activity)
                 .HasForeignKey("ActivityId")
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
