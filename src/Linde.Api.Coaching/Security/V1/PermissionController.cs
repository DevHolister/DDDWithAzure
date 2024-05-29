using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Security.Permission.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Security.V1;

[Authorize]
public class PermissionController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        (await Mediator!.Send(new GetAllPermissionQuery()))
        .Match(
            result => Ok(result),
            error => Problem(error));
}
