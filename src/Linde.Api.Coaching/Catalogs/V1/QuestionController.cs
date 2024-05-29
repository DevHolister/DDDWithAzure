using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Activities.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Divisions.Commands.Create;
using Linde.Core.Coaching.Catalogs.Divisions.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Delete;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Questions.Commands.Save;
using Linde.Core.Coaching.Catalogs.Questions.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Questions.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Questions.Queries.GetById;
using Linde.Domain.Coaching.Common;
using Linde.Infrastructure.Coaching.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Linde.Api.Coaching.Catalogs.V1
{
    [Authorize]
    public class QuestionController : BaseApiController
    {
        [HttpGet]
        [HasPermission(CatalogPermission.ReadQuestion)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQuestionsQuery query)
        {
            var response = await Mediator!.Send(query);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        [HasPermission(CatalogPermission.CreateQuestion)]
        public async Task<IActionResult> Create(SaveQuestionCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpPut]
        [HasPermission(CatalogPermission.WriteQuestion)]
        public async Task<IActionResult> Edit(EditQuestionCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }
        [HttpDelete("{id:guid}")]
        [HasPermission(CatalogPermission.DeleteQuestion)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator!.Send(new DeleteQuestionCommand() { QuestionId = id});
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }
        [HttpGet("{id:guid}")]
        [HasPermission(CatalogPermission.ReadQuestion)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await Mediator!.Send(new GetQuestionByIdQuery(){ QuestionId = id });
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("{term}/Autocomplete")]
        public async Task<IActionResult> GetAutocomplete(string term)
        {
            var response = await Mediator!.Send(new QuestionAutocompleteQuery(term));
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
