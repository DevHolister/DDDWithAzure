using ErrorOr;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Common;
using Linde.Core.Coaching.Security.User.Queries.Autocomplete;
using Linde.Core.Coaching.Security.User.Specifications;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Security.User.Queries.AutoCompleteFilters
{
    internal class GetUserAutoCompleteFiltersQueryHandler : IRequestHandler<GetUserAutoCompleteFiltersQuery, ErrorOr<List<UserAutocompleteByFiltersDto>>>
    {
        private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUserAutoCompleteFiltersQueryHandler> _logger;

        public GetUserAutoCompleteFiltersQueryHandler(
       IRepository<Domain.Coaching.UserAggreagate.User> repository,
       IMapper mapper,
       ICurrentUserService currentUserService,
       ILogger<GetUserAutoCompleteFiltersQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<ErrorOr<List<UserAutocompleteByFiltersDto>>> Handle(GetUserAutoCompleteFiltersQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _repository.ListAsync(new UserWhereSpecification(request.Name, request.UserLindeId, request.NoEmployee));
                var response = _mapper.Map<List<UserAutocompleteByFiltersDto>>(user);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
