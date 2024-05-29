using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Catalogs.Questions.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models;
using Linde.Domain.Coaching.Questions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Queries.Autocomplete
{
    internal class QuestionAutocompleteQueryHandler : IRequestHandler<QuestionAutocompleteQuery, ErrorOr<List<ItemDto>>>
    {
        private readonly IRepository<CatQuestions> _repository;
        private readonly ILogger<QuestionAutocompleteQueryHandler> _logger;

        public QuestionAutocompleteQueryHandler(
        IRepository<CatQuestions> repository,
        ILogger<QuestionAutocompleteQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<ErrorOr<List<ItemDto>>> Handle(QuestionAutocompleteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var questions = await _repository.ListAsync(new QuestionAutocompleteSpecifiction(
                    request.name!), cancellationToken);

                return new List<ItemDto>(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
