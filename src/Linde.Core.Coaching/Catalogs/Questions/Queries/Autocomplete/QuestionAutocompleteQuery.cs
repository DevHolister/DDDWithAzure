using ErrorOr;
using Linde.Core.Coaching.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.Autocomplete
{
    public record QuestionAutocompleteQuery(string name) : IRequest<ErrorOr<List<ItemDto>>>;
}
