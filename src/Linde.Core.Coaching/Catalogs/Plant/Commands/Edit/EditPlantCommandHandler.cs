using ErrorOr;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Commands.Edit;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Domain.Coaching.CountryAggregate;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Plant.Commands.Edit
{
    internal class EditPlantCommandHandler : IRequestHandler<EditPlantCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Domain.Coaching.Entities.Catalogs.Plant> _plantRepository;
        private readonly ILogger<EditPlantCommandHandler> _logger;

        public EditPlantCommandHandler(
        IRepository<Domain.Coaching.Entities.Catalogs.Plant> plantRepository,
        ILogger<EditPlantCommandHandler> logger)
        {
            _plantRepository = plantRepository;    
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(EditPlantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var plant = Domain.Coaching.Entities.Catalogs.Plant.Update(
                    Guid.Parse(request.PlantId),
                    request.Name,
                    request.Bu,
                    CountryId.Create(Guid.Parse(request.CountryId)),
                    request.Division,
                    request.State,
                    request.City,
                    request.Municipality,
                    UserId.Create(Guid.Parse(request.SuperintendentId)),
                    UserId.Create(Guid.Parse(request.PlantManagerId))
                );


                await _plantRepository.UpdateAsync(plant);

                return plant.PlantId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
