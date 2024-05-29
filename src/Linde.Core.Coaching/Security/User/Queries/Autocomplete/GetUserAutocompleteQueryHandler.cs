using ErrorOr;
using Linde.Core.Coaching.Common.Models.Security.User;
using Linde.Core.Coaching.Common.Models;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Core.Coaching.Common.Interfaces.Services;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Linde.Core.Coaching.Common.Models.Common;

namespace Linde.Core.Coaching.Security.User.Queries.Autocomplete
{
    internal class GetUserAutocompleteQueryHandler : IRequestHandler<GetUserAutocompleteQuery, ErrorOr<List<UserAutocompleteDto>>>
    {
        private readonly IRepository<Domain.Coaching.UserAggreagate.User> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUserAutocompleteQueryHandler> _logger;

        public GetUserAutocompleteQueryHandler(
       IRepository<Domain.Coaching.UserAggreagate.User> repository,
       IMapper mapper,
       ICurrentUserService currentUserService,
       ILogger<GetUserAutocompleteQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<ErrorOr<List<UserAutocompleteDto>>> Handle(GetUserAutocompleteQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _repository.ListAsync(new UserWhereSpecification(request.FullName));
                var response = _mapper.Map<List<UserAutocompleteDto>>(user);

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
