using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Security.Role.Commands.Create;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Security.Role.Commands.Edit;
using Linde.Core.Coaching.Security.Role.Queries.Autocomplete;
using Linde.Core.Coaching.Security.Role.Queries.GetAll;
using Linde.Core.Coaching.Security.Role.Queries.GetById;
using Linde.Core.Coaching.Security.Role.Queries.GetEnumerable;
using Linde.Core.Coaching.Security.User.Queries.AutocompleteView;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Security.V1;

[Authorize]
public class RoleController : BaseApiController
{
    [HttpGet]
    [HasPermission(CatalogPermission.ReadRole)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRolesQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }

    [HttpGet]
    [Route("GetRoleEnumerable")]
    [HasPermission(CatalogPermission.ReadRole)]
    public async Task<IActionResult> GetEnumerable()
    {
        var response = await Mediator!.Send(new GetEnumerableRoleQuery());
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }

    [HttpPost]
    [HasPermission(CatalogPermission.CreateRole)]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            error => Problem(error));
    }

    [HttpPut]
    [HasPermission(CatalogPermission.WriteRole)]
    public async Task<IActionResult> Update(EditRoleCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(CatalogPermission.DeleteRole)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await Mediator!.Send(new DeleteRoleCommand(id));
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }
    
    [HttpGet("{id:guid}")]
    [HasPermission(CatalogPermission.ReadRole)]
    public async Task<IActionResult> GetUserByRole([FromRoute] Guid id)
    {
        var response = await Mediator!.Send(new GetRoleByIdQuery(id));
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }

    [HttpGet]
    [Route("{name}/Autocomplete")]
    [HasPermission(CatalogPermission.ReadRole)]
    public async Task<IActionResult> GetAutocomplete(string name)
    {
        var response = await Mediator!.Send(new GetRoleAutocompleteQuery(name));
        //var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
