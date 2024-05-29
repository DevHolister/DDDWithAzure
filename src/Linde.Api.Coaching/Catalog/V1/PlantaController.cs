using Linde.Api.Coaching.Controllers;
using Linde.Core.Coaching.Catalogs.Operador.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Plant.Commands.Create;
using Linde.Core.Coaching.Catalogs.Plant.Commands.Edit;
using Linde.Core.Coaching.Catalogs.Plant.Delete;
using Linde.Core.Coaching.Catalogs.Plant.Queries.Autocomplete;
using Linde.Core.Coaching.Catalogs.Plant.Queries.FindAtPlantView;
using Linde.Core.Coaching.Catalogs.Plant.Queries.GetAll;
using Linde.Core.Coaching.Catalogs.Plant.Queries.GetByID;
using Linde.Core.Coaching.Catalogs.Plant.Queries.GetEnumerable;
using Linde.Core.Coaching.Security.Role.Commands.Delete;
using Linde.Core.Coaching.Security.User.Commands.Create;
using Linde.Core.Coaching.Security.User.Commands.Edit;
using Linde.Core.Coaching.Security.User.Queries.Autocomplete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Linde.Api.Coaching.Catalog.V1
{

    [Authorize]
    public class PlantaController : BaseApiController
    {
        [HttpGet]
        //TODO: [HasPermission(CatalogPermission.ReadUser)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPlantQuery query)
        {
            var response = await Mediator!.Send(query);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("FindViewPlant")]
        public async Task<IActionResult> FindCountryAtUsersView(string code)
        {
            var response = await Mediator!.Send(new FindAtPlantsViewQuery { Code = code });
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await Mediator!.Send(new GetPlantByIdQuery() { PlantID = id });
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("GetPlantEnumerable")]
        public async Task<IActionResult> GetEnumerable(GetEnumerablePlantQuery model)
        {
            var response = await Mediator!.Send(model);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        //[HasPermission(CatalogPermission.WriteUser)]
        public async Task<IActionResult> Create(CreatePlantCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => StatusCode(StatusCodes.Status201Created, result),
                errors => Problem(errors));
        }

        [HttpPut]
        public async Task<IActionResult> Update(EditPlantCommand command)
        {
            var response = await Mediator!.Send(command);
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        //[HasPermission(CatalogPermission.DeleteRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator!.Send(new DeletePlantCommand(id));
            return response.Match(
                result => Ok(result),
                error => Problem(error));
        }

        [HttpGet]
        [Route("{fullname}/Autocomplete/{countryId?}")]
        public async Task<IActionResult> Autocomplete(string fullname, Guid? countryId)
        {
            var response = await Mediator!.Send(new GetPlantAutocompleteQuery(fullname, countryId.ToString()));
            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
