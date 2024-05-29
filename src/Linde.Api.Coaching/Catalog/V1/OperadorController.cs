using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Create;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Operador.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Operador.Queries.GetAll;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Core.Coaching.Security.User.Commands.Edit;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.RoleAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.ValueObjects;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Linde.Api.Coaching.Catalog.V1
{

    [Authorize]
    public class OperadorController : BaseApiController
    {
        [HttpGet]
        [HasPermission(CatalogPermission.ReadOperator)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOperadorQuery query)
        {
            var response = await Mediator!.Send(query);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        [HasPermission(CatalogPermission.CreateOperator)]
        public async Task<IActionResult> Create(CreateOperatorCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpDelete("{userid:guid}")]
        [HasPermission(CatalogPermission.DeleteOperator)]
        public async Task<IActionResult> Delete(Guid userid)
        {
            var response = await Mediator!.Send(new DeleteOperadorCommand(UserId.Create(userid)));
            return response.Match(
                result => Ok(result),
                error => Problem(error));
        }
    }
}
