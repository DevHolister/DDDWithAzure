using Linde.Domain.Coaching.DivisionAggregate;
using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Persistence.Coaching.Configurations
{
    public class MenuAggregateConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("TBL_MENUS", Constants.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("MenuId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => MenuId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Path)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Level)
            .IsRequired();

            builder.Property(x => x.ParentId)
            .HasConversion(x => x.Value, value => MenuId.Create(value))
            .HasDefaultValue(null);

            builder.Property(x => x.Icon)
            .HasMaxLength(30)
            .HasDefaultValue(null);

            builder.Property(x => x.ContainsChildren)
            .IsRequired()
            .HasDefaultValue(false);

            builder.Property(x => x.PermissionId)
            .HasDefaultValue(null);

            builder.Ignore(x => x.SpecifyCreatedBy);
            builder.HasMany<Menu>(x => x.Attributes).WithOne(x => x.Attribute).HasForeignKey(x => x.ParentId);
            builder.HasOne(x => x.Permissions)
                 .WithOne(a => a.Menu)
                 .HasForeignKey<Menu>(a => a.PermissionId);
        }
    }
}
