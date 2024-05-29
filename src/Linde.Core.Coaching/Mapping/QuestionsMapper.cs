using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models.Menu;
using Linde.Domain.Coaching.ActivityAggregate.ValueObjects;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Mapping
{
    public class QuestionsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CatQuestions, QuestionDto>()
                .Map(x => x.QuestionId, src => src.Id.Value)
                .Map(x => x.Activities, src => GetActivities(src.QuestionsActivities.ToList()));
        }

        private List<ItemDto> GetActivities(List<QuestionsActivities> model)
        {
            var items = model
                .Select(x => new ItemDto(x.ActivityId.Value, x.Activity.Name));

            return items.ToList();
        }
    }
}
