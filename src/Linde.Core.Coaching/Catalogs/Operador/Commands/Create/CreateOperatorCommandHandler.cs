using ErrorOr;
using Linde.Core.Coaching.Catalogs.Country.Specifications;
using Linde.Core.Coaching.Catalogs.Operador.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Core.Coaching.Security.User.Specifications;
using Linde.Domain.Coaching.CountryAggregate.ValueObjects;
using Linde.Domain.Coaching.Entities.Catalogs;
using Linde.Domain.Coaching.RoleAggregate;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Core.Coaching.Catalogs.Operador.Commands.Create
{
    internal class CreateOperatorCommandHandler : IRequestHandler<CreateOperatorCommand, ErrorOr<bool>>
    {
        private readonly IRepository<UserPlant> _plantRepository;
        private readonly IRepository<Domain.Coaching.Entities.Catalogs.UserPlant> _operatorRepository;
        private readonly ILogger<CreateOperatorCommandHandler> _logger;

        public CreateOperatorCommandHandler(
        IRepository<UserPlant> plantRepository,
        IRepository<Domain.Coaching.Entities.Catalogs.UserPlant> operatorRepository,
        IRepository<Role> roleRepository,
        ILogger<CreateOperatorCommandHandler> logger)
        {
            _plantRepository = plantRepository;
            _operatorRepository = operatorRepository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userPlants = await  _operatorRepository.ListAsync(new OperadorWhereSpecification(UserId.Create(request.UserId)));

                if(userPlants == null)
                {
                    return Security.User.Errors.User.UserNotFound;
                }

                foreach (var item in userPlants)
                {
                    item.UpdateOperator(true);
                    //await _operatorRepository.UpdateAsync(item);
                }
                await _operatorRepository.UpdateRangeAsync(userPlants);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
