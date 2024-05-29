using ErrorOr;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using Linde.Domain.Coaching.Views;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.FindAtPlantView
{
    internal class FindAtPlantsViewQueryHandler : IRequestHandler<FindAtPlantsViewQuery, ErrorOr<List<PlantDTO>>>
    {
        private readonly ILogger<FindAtPlantsViewQueryHandler> _logger;
        private readonly IRepository<VwPlantas> _repositoryView;
        private readonly IMapper _mapper;

        public FindAtPlantsViewQueryHandler(
            ILogger<FindAtPlantsViewQueryHandler> logger,
            IRepository<VwPlantas> repositoryView,
            IMapper mapper)
        {
            _logger = logger;
            _repositoryView = repositoryView;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<PlantDTO>>> Handle(FindAtPlantsViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                

                var findCodeCountry = await _repositoryView.ListAsync(new PlantViewSpecification(request.Code));
                if (findCodeCountry == null)
                {
                    return Default.NotFound;
                }


                var item = _mapper.Map<List<PlantDTO>>(findCodeCountry!);

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
