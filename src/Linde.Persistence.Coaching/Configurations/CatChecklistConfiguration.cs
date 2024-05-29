using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Persistence.Coaching.Configurations
{
    public class CatChecklistConfiguration : IEntityTypeConfiguration<CatChecklist>
    {
        public void Configure(EntityTypeBuilder<CatChecklist> builder)
        {
            ConfigureChecklistTable(builder);
        }

        private void ConfigureChecklistTable(EntityTypeBuilder<CatChecklist> builder)
        {
            builder.ToTable("CAT_CHECKLISTS", Constants.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ChecklistId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => ChecklistId.Create(value));

            builder.Property(x => x.Name)
                 .HasMaxLength(1500)
                 .IsRequired();

            builder.Property(x => x.Description)
                 .HasMaxLength(1500)
                 .IsRequired();


            builder.HasMany(x => x.ChecklistsQuestions)
                 .WithOne(x => x.Checklists)
                 .HasForeignKey(x => x.CatChecklistId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.FindNavigation(nameof(CatChecklist.ChecklistsQuestions))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(x => x.SpecifyCreatedBy);
        }
    }
}
