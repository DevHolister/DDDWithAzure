using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.MenuAggregate;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
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
    public class QuestionAcitivityConfiguration : IEntityTypeConfiguration<QuestionsActivities>
    {
        public void Configure(EntityTypeBuilder<QuestionsActivities> builder)
        {
            builder.ToTable("ASOC_QUESTIONS_ACTIVITIES", Constants.Schema);

            builder.HasKey("CatQuestionsId", "ActivityId");

            builder.Property(x => x.CatQuestionsId)
                .HasColumnName("CatQuestionsId")
                .HasConversion(id => id.Value, value => QuestionId.Create(value));

            builder.Property(x => x.ActivityId)
                .HasColumnName("ActivityId")
                .HasConversion(id => id.Value, value => ActivityId.Create(value));

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
