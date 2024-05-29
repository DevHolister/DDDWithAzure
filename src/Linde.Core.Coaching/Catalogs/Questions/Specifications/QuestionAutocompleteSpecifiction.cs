using Ardalis.Specification;
using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.Questions;

namespace Linde.Core.Coaching.Catalogs.Questions.Specifications
{
    public class QuestionAutocompleteSpecifiction : Specification<CatQuestions, ItemDto>
    {
        public QuestionAutocompleteSpecifiction(string name)
        {
            Query
                .Select(x => new ItemDto(
                    x.Id.Value,
                    x.Name
                    ))
                .AsNoTracking();
            Query.Where(x => x.Name.Contains(name) && x.Visible);
        }
    }
}
