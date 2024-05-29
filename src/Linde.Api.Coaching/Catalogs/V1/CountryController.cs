using Linde.Api.Coaching.Controllers;
using Microsoft.AspNetCore.Mvc;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Linde.Core.Coaching.Catalogs.Country.Commands.Create;
using Linde.Core.Coaching.Catalogs.Country.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Country.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Country.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Country.Queries.FindById;
using Linde.Core.Coaching.Catalogs.Country.Queries.GetEnumerable;
using Linde.Core.Coaching.Catalogs.Country.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Country.Queries.FindAtUsersView;

namespace Linde.Api.Coaching.Catalogs.V1;

[Authorize]
public class CountryController : BaseApiController
{
    [HttpGet]
    [HasPermission(CatalogPermission.ReadCountry)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCountryQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("GetCountryEnumerable")]
    [HasPermission(CatalogPermission.ReadCountry)]
    public async Task<IActionResult> GetEnumerable()
    {
        var response = await Mediator!.Send(new GetEnumerableCountryQuery());
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("FindCountry")]
    [HasPermission(CatalogPermission.ReadCountry)]
    public async Task<IActionResult> FindCountry(Guid countryId)
    {
        var response = await Mediator!.Send(new FindByIdQuery { CountryId = countryId });
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("FindCountryAtUsersView")]
    [HasPermission(CatalogPermission.ReadCountry)]
    public async Task<IActionResult> FindCountryAtUsersView(string code)
    {
        var response = await Mediator!.Send(new FindAtUsersViewQuery { Code = code });
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

[HttpPost]
    [HasPermission(CatalogPermission.CreateCountry)]
    public async Task<IActionResult> CreateCountry(CreateCountryCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            errors => Problem(errors));
    }

    [HttpPut]
    [HasPermission(CatalogPermission.WriteCountry)]
    public async Task<IActionResult> UpdateCountry(EditCountryCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpDelete]
    [HasPermission(CatalogPermission.DeleteCountry)]
    public async Task<IActionResult> DeleteCountry(Guid countryId)
    {
        var response = await Mediator!.Send(new DeleteCountryCommand { CountryId = countryId });
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("{fullname}/Autocomplete")]
    public async Task<IActionResult> Autocomplete(string fullname)
    {
        var response = await Mediator!.Send(new GetCountryAutocompleteQuery(fullname));
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
