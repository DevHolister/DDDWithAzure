using Linde.Core.Coaching.Common.Models.Catalogs.Questions;
using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.Questions;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Domain.Coaching.ChecklistAggregate;
using Linde.Core.Coaching.Common.Models.Catalogs.Checklist;
using Linde.Domain.Coaching.ChecklistAggregate.Entities;

namespace Linde.Core.Coaching.Mapping
{
    public class ChecklistMapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CatChecklist, ChecklistDto>()
                .Map(x => x.ChecklistId, src => src.Id.Value)
                .Map(x => x.Questions, src => GetQuestions(src.ChecklistsQuestions.ToList()));
        }

        private List<ItemDto> GetQuestions(List<ChecklistsQuestions> model)
        {
            var items = model
                .Select(x => new ItemDto(x.QuestionId.Value, x.Question.Name));

            return items.ToList();
        }
    }
}
