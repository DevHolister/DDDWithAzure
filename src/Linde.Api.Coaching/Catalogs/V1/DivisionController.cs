using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Create;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Divisions.Queries.GetAll;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Catalogs.V1;

[Authorize]
public class DivisionController : BaseApiController
{

    [HttpGet]
    [HasPermission(CatalogPermission.ReadDivision)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllDivisionsQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(CatalogPermission.CreateDivision)]
    public async Task<IActionResult> Create(CreateDivisionCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            errors => Problem(errors));
    }

    [HttpPut]
    [HasPermission(CatalogPermission.WriteDivision)]
    public async Task<IActionResult> Update(EditDivisionCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(CatalogPermission.DeleteDivision)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await Mediator!.Send(new DeleteDivisionCommand(id));
        return response.Match(
            result => Ok(result),
            error => Problem(error));
    }

}
