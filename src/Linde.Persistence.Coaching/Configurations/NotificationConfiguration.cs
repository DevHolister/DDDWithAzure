using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Persistence.Coaching.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<TblNotification>
    {
        public void Configure(EntityTypeBuilder<TblNotification> builder)
        {
            builder.ToTable("TBL_NOTIFICATIONS", Constants.Schema);
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.NotificationId).ValueGeneratedOnAdd();

            builder.Property(x => x.Subject)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Ignore(x => x.SpecifyCreatedBy);
            builder.Ignore(x => x.ModifiedBy);
            builder.Ignore(x => x.ModifiedAt);

        }
    }
}
