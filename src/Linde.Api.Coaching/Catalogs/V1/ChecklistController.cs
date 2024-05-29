using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Checklists.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Checklists.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Checklists.Commands.Save;
using Linde.Core.Coaching.Catalogs.Checklists.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Checklists.Queries.GetById;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Catalogs.V1
{
    [Authorize]
    public class ChecklistController : BaseApiController
    {
        [HttpGet]
        [HasPermission(CatalogPermission.ReadChecklist)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCheklistsQuery query)
        {
            var response = await Mediator!.Send(query);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        [HasPermission(CatalogPermission.CreateChecklist)]
        public async Task<IActionResult> Create(SaveChecklistCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpPut]
        [HasPermission(CatalogPermission.WriteChecklist)]
        public async Task<IActionResult> Edit(EditChecklistCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }
        [HttpDelete("{id:guid}")]
        [HasPermission(CatalogPermission.DeleteChecklist)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator!.Send(new DeleteChecklistCommand() { ChecklistId = id });
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }
        [HttpGet("{id:guid}")]
        [HasPermission(CatalogPermission.ReadChecklist)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await Mediator!.Send(new GetChecklistByIdQuery() { QuestionId = id });
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }
    }
}
