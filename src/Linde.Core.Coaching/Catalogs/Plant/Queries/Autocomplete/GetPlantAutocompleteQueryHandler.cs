using ErrorOr;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
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

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.Autocomplete
{
    internal class GetPlantAutocompleteQueryHandler : IRequestHandler<GetPlantAutocompleteQuery, ErrorOr<List<PlantAutocompleteDto>>>
    {
        private readonly IRepository<Domain.Coaching.Entities.Catalogs.Plant> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetPlantAutocompleteQueryHandler> _logger;

        public GetPlantAutocompleteQueryHandler(
       IRepository<Domain.Coaching.Entities.Catalogs.Plant> repository,
       IMapper mapper,
       ICurrentUserService currentUserService,
       ILogger<GetPlantAutocompleteQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<ErrorOr<List<PlantAutocompleteDto>>> Handle(GetPlantAutocompleteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var plant = await _repository.ListAsync(new PlantWhereSpecification(request.FullName, request.CountryId));
                var response = _mapper.Map<List<PlantAutocompleteDto>>(plant);

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
