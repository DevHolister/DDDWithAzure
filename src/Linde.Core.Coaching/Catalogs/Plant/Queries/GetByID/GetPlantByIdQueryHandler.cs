using ErrorOr;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Common.Models.Catalog.Plant;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Queries.GetByID
{

    internal class GetPlantByIdQueryHandler : IRequestHandler<GetPlantByIdQuery, ErrorOr<PlantDTO>>
    {
        private readonly ILogger<GetPlantByIdQueryHandler> _logger;
        private readonly IRepository<Linde.Domain.Coaching.Entities.Catalogs.Plant> _repository;
        private readonly IMapper _mapper;

        public GetPlantByIdQueryHandler(
            ILogger<GetPlantByIdQueryHandler> logger,
            IRepository<Linde.Domain.Coaching.Entities.Catalogs.Plant> repository,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PlantDTO>> Handle(GetPlantByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await _repository.FirstOrDefaultAsync(new PlantMapSpecification(Guid.Parse(request.PlantID)), cancellationToken);

                if (question is null)
                {
                    return Default.NotFound;
                }

                var response = _mapper.Map<PlantDTO>(question);

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