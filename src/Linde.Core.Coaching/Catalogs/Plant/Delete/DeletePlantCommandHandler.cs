using ErrorOr;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Core.Coaching.Catalogs.Plant.Specifications;
using System.Reflection;

namespace Linde.Core.Coaching.Catalogs.Plant.Delete
{
    internal class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Linde.Domain.Coaching.Entities.Catalogs.Plant> _repository;
        private readonly ILogger<DeletePlantCommandHandler> _logger;
        public DeletePlantCommandHandler(
        IRepository<Linde.Domain.Coaching.Entities.Catalogs.Plant> repository,
        ILogger<DeletePlantCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<ErrorOr<Guid>> Handle(DeletePlantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var plant = await _repository.FirstOrDefaultAsync(new PlantWhereSpecification(request.Id));

                if (plant is null)
                    return Default.NotFound;

                
                await _repository.DeleteAsync(plant!);

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
