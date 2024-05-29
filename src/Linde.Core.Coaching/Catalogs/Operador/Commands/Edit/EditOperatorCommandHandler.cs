using ErrorOr;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Create;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Operador.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Domain.Coaching.DivisionAggregate;
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

namespace Linde.Core.Coaching.Catalogs.Operador.Commands.Edit
{
    internal class EditOperatorCommandHandler : IRequestHandler<EditOperatorCommand, ErrorOr<Guid>>
    {       
        private readonly IRepository<UserPlant> _operatorRepository;
        private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _rolerepository;
        private readonly ILogger<EditOperatorCommandHandler> _logger;

        public EditOperatorCommandHandler(
        IRepository<UserPlant> operatorRepository,
        IRepository<Domain.Coaching.RoleAggregate.Role> rolrepository,
        ILogger<EditOperatorCommandHandler> logger)
        {
            _operatorRepository = operatorRepository;
            _rolerepository = rolrepository;
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(EditOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _rolerepository.FirstOrDefaultAsync(new RoleWhereSpecification("Operador"));

                var roleId = RoleId.Create(role.Id.Value);

                var otro = new OperadorWhereSpecification(
                    UserId.Create(request.UserId),
                    request.CurrentPlantId);


                var editoperator = await _operatorRepository.FirstOrDefaultAsync(new OperadorWhereSpecification(
                    UserId.Create(request.UserId),
                    request.CurrentPlantId));

                if (editoperator is null)
                    return Default.NotFound;
                await _operatorRepository.DeleteAsync(editoperator);
                _operatorRepository.SaveChangesAsync(cancellationToken);

                editoperator.UpdatePlant(request.PlantId);
                await _operatorRepository.AddAsync(editoperator);

                return editoperator.UserId.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
