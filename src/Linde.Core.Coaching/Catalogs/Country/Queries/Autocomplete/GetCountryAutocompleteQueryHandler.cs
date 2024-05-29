using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Catalogs.Plant.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Interfaces.Services;
using Linde.Core.Coaching.Common.Models.Common;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Country.Queries.Autocomplete
{
    internal class GetCountryAutocompleteQueryHandler : IRequestHandler<GetCountryAutocompleteQuery, ErrorOr<List<CountryAutocompleteDto>>>
    {
        private readonly IRepository<Domain.Coaching.CountryAggregate.Country> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetCountryAutocompleteQueryHandler> _logger;

        public GetCountryAutocompleteQueryHandler(
       IRepository<Domain.Coaching.CountryAggregate.Country> repository,
       IMapper mapper,
       ICurrentUserService currentUserService,
       ILogger<GetCountryAutocompleteQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ErrorOr<List<CountryAutocompleteDto>>> Handle(GetCountryAutocompleteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var plant = await _repository.ListAsync(new CountryWhereSpecification(request.FullName));
                var response = _mapper.Map<List<CountryAutocompleteDto>>(plant);

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
