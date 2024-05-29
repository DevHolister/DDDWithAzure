using ErrorOr;
using Linde.Core.Coaching.Catalogs.Operador.Specifications;
using Linde.Core.Coaching.Common.Errors;
using Linde.Core.Coaching.Common.Interfaces.Persistence;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Security.Role.Specifications;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Operador.Commands.Delete
{
    internal class DeleteOperadorCommandHandler : IRequestHandler<DeleteOperadorCommand, ErrorOr<Unit>>
    {
        private readonly IRepository<Linde.Domain.Coaching.Entities.Catalogs.UserPlant> _repository;
        private readonly IRepository<Domain.Coaching.RoleAggregate.Role> _rolerepository;
        private readonly ILogger<DeleteOperadorCommandHandler> _logger;

        public DeleteOperadorCommandHandler(
        IRepository<Linde.Domain.Coaching.Entities.Catalogs.UserPlant> repository,
        IRepository<Domain.Coaching.RoleAggregate.Role> rolrepository,
        ILogger<DeleteOperadorCommandHandler> logger)
        {
            _repository = repository;
            _rolerepository = rolrepository;
            _logger = logger;
        }


        public async Task<ErrorOr<Unit>> Handle(DeleteOperadorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userPlants = await _repository.ListAsync(new OperadorWhereSpecification(request.UserId));

                if (userPlants == null)
                {
                    return Security.User.Errors.User.UserNotFound;
                }

                foreach (var item in userPlants)
                {
                    item.UpdateOperator(false);
                }

                await _repository.UpdateRangeAsync(userPlants);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return Default.ServerError;
            }
        }
    }
}
