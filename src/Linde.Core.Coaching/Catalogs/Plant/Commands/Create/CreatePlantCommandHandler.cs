using ErrorOr;
using Linde.Core.Coaching.Catalogs.Plant.Commands.Edit;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Commands.Create;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Plant.Commands.Create
{
    public record CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Domain.Coaching.Entities.Catalogs.Plant> _plantRepository;
        private readonly ILogger<CreatePlantCommandHandler> _logger;

        public CreatePlantCommandHandler(
        IRepository<Domain.Coaching.Entities.Catalogs.Plant> plantRepository,
        ILogger<CreatePlantCommandHandler> logger)
        {
            _plantRepository = plantRepository;
            _logger = logger;
        }
        public async Task<ErrorOr<Guid>> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var newplant = Domain.Coaching.Entities.Catalogs.Plant.Create(
                    Guid.NewGuid(),
                    request.Name,
                    request.Bu,
                    CountryId.Create(Guid.Parse("A01AD815-184B-4FE8-954E-2CA5A44E15A8")),
                    request.Division,
                    request.State,
                    request.City,
                    request.Municipality,
                    UserId.Create(Guid.Parse(request.SuperintendentId)),
                    UserId.Create(Guid.Parse(request.PlantManagerId))
                );

                await _plantRepository.AddAsync(newplant);

                return newplant.PlantId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
