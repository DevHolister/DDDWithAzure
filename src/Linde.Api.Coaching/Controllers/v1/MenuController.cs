using Linde.Core.Coaching.Menu.Queries.GetByUserId;
using Linde.Core.Coaching.Security.User.Queries.GetAll;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Controllers.v1
{
    [Authorize]
    public class MenuController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetMenuByUser()
        {
            var response = await Mediator!.Send(new GetMenuByUserIdQuery());
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
