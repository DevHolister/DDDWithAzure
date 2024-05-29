using ErrorOr;
using Linde.Core.Coaching.Catalogs.Activities.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Activities.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalog.Activity;
using Linde.Core.Coaching.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Linde.Core.Coaching.Catalogs.Activities.Queries.Autocomplete
{
    internal class ActivityAutocompleteQueryHandler : IRequestHandler<ActivityAutocompleteQuery, ErrorOr<List<ItemDto>>>
    {
        private readonly IRepository<Domain.Coaching.ActivityAggregate.Activity> _repository;
        private readonly ILogger<ActivityAutocompleteQueryHandler> _logger;

        public ActivityAutocompleteQueryHandler(
        IRepository<Domain.Coaching.ActivityAggregate.Activity> repository,
        ILogger<ActivityAutocompleteQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<List<ItemDto>>> Handle(ActivityAutocompleteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var activities = await _repository.ListAsync(new ActivityAutocompleteSpecifiction(
                    request.name!), cancellationToken);

                return new List<ItemDto>(activities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
