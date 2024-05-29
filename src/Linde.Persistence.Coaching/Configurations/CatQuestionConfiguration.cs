using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linde.Persistence.Coaching.Configurations
{
    public class CatQuestionConfiguration : IEntityTypeConfiguration<CatQuestions>
    {
        public void Configure(EntityTypeBuilder<CatQuestions> builder)
        {
            ConfigureQuestionTable(builder);
        }

        private void ConfigureQuestionTable(EntityTypeBuilder<CatQuestions> builder)
        {
            builder.ToTable("CAT_QUESTIONS", Constants.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("QuestionId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => QuestionId.Create(value));

            builder.Property(x => x.Name)
                 .HasMaxLength(1500)
                 .IsRequired();

            builder.Property(x => x.IsCritical)
                .IsRequired();

            builder.HasMany(x => x.QuestionsActivities)
                 .WithOne(x => x.Questions)
                 .HasForeignKey(x => x.CatQuestionsId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.FindNavigation(nameof(CatQuestions.QuestionsActivities))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(x => x.SpecifyCreatedBy);
        }
    }
}
