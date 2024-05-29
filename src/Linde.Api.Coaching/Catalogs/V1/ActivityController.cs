using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Activities.Commands.Create;
using Linde.Core.Coaching.Catalogs.Activities.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Activities.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Activities.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Activities.Queries.GetAll;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Catalogs.V1;
[Authorize]
public class ActivityController : BaseApiController
{
    [HttpGet]
    [HasPermission(CatalogPermission.ReadActivity)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllActivitiesQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }


    [HttpPost]
    [HasPermission(CatalogPermission.CreateActivity)]
    public async Task<IActionResult> CreateActivity(CreateActivityCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            errors => Problem(errors));
    }

    [HttpPut]
    [HasPermission(CatalogPermission.WriteActivity)]
    public async Task<IActionResult> UpdateActivity(EditActivityCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(CatalogPermission.DeleteActivity)]
    public async Task<IActionResult> DeleteActivity(Guid id)
    //(DeleteCountryCommand command)
    {
        var response = await Mediator!.Send(new DeleteActivityCommand(id));
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("{term}/Autocomplete")]
    public async Task<IActionResult> GetAutocomplete(string term)
    {
        var response = await Mediator!.Send(new ActivityAutocompleteQuery(term));
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
