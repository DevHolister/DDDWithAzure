using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Security.User.Queries.FindById;
using Linde.Core.Coaching.Security.User.Commands.Authenticate;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Core.Coaching.Security.User.Commands.Delete;
using Linde.Core.Coaching.Security.User.Commands.Edit;
using Linde.Core.Coaching.Security.User.Commands.Lockout;
using Linde.Core.Coaching.Security.User.Queries.Autocomplete;
using Linde.Core.Coaching.Security.User.Queries.FindAtAD;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Linde.Core.Coaching.Security.User.Queries.FindAtUsersView;
using Linde.Core.Coaching.Security.User.Queries.AutocompleteView;
using Linde.Core.Coaching.Catalogs.Country.Queries.GetAllUsersInView;
using Linde.Core.Coaching.Security.User.Queries.AutoCompleteFilters;

namespace Linde.Api.Coaching.Security.V1;

[Authorize]
public class UserController : BaseApiController
{
    [HttpGet]
    [HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("{fullname}/Autocomplete")]
    public async Task<IActionResult> Autocomplete(string fullname)
    {
        var response = await Mediator!.Send(new GetUserAutocompleteQuery(fullname));
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("FindAtAD")]
    [HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> FindUser([FromQuery] FindUserAtADQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("FindUser")]
    [HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> FindUser(Guid userId)
    {
        var response = await Mediator!.Send(new FindByIdQuery { UserId = userId });
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(CatalogPermission.WriteUser)]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => StatusCode(StatusCodes.Status201Created, result),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("Auth")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(AuthenticateCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EditUserCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPut]
    [Route("Lockout")]
    [HasPermission(CatalogPermission.WriteUser)]
    public async Task<IActionResult> Lockout(LockoutUserCommand command)
    {
        var response = await Mediator!.Send(command);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
    [HttpDelete]
    [HasPermission(CatalogPermission.DeleteUser)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var response = await Mediator!.Send(new DeleteUserCommand { UserId = userId });
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("FindAtUsersView")]
    [HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> FindAtUsersView(string userName)
    {
        FindAtUsersViewQuery query = new FindAtUsersViewQuery(userName);
        //var response = await Mediator!.Send(new FindAtUsersViewQuery { UserName = userName });
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("{fullName}/AutocompleteView")]
    [HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> FindByNameAtView(string fullName)
    {
        var response = await Mediator!.Send(new FindByNameAtViewQuery(fullName));
        //var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
    [HttpGet]
    [Route("AutocompleteFilters")]
    //[HasPermission(CatalogPermission.ReadUser)]
    public async Task<IActionResult> AutocompleteFilters([FromQuery] GetUserAutoCompleteFiltersQuery query)
    {
        var response = await Mediator!.Send(query);
        return response.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
