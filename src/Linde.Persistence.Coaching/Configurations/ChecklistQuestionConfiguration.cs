using Linde.Domain.Coaching.ChecklistAggregate.Entities;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations
{
    public class ChecklistQuestionConfiguration : IEntityTypeConfiguration<ChecklistsQuestions>
    {
        public void Configure(EntityTypeBuilder<ChecklistsQuestions> builder)
        {
            builder.ToTable("ASOC_CHECKLISTS_QUESTIONS", Constants.Schema);

            builder.HasKey("CatChecklistId", "QuestionId");

            builder.Property(x => x.CatChecklistId)
                .HasColumnName("CatChecklistId")
                .HasConversion(id => id.Value, value => ChecklistId.Create(value));

            builder.Property(x => x.QuestionId)
                .HasColumnName("QuestionId")
                .HasConversion(id => id.Value, value => QuestionId.Create(value));

            builder.Property(x => x.CreatedBy)
                    .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Ignore(x => x.SpecifyCreatedBy);
            builder.Ignore(x => x.ModifiedAt);
            builder.Ignore(x => x.ModifiedBy);
            builder.Ignore(x => x.Visible);
            builder.Ignore(x => x.Id);
        }
    }
}
