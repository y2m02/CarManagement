using System.Threading.Tasks;
using CarManagementApi.Models.Dtos;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    public class ModelController : BaseApiController
    {
        private readonly IModelService service;

        public ModelController(IModelService service) => this.service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50
        )
        {
            return OkResponse(await service.GetAll(pageNumber, pageSize).ConfigureAwait(false));
        }

        [HttpGet("{id:required}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetById(id).ConfigureAwait(false);

            if (response.Failed())
            {
                return InternalServerError(response);
            }

            return response.NotFound() ? NotFound(new { id }) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelRequest request)
        {
            var response = await service.Add(request).ConfigureAwait(false);

            if (response is not Success success)
            {
                return InternalServerError(response);
            }

            var model = (ModelDto)success.Response;

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { controller = "Model", id = model.Id },
                value: model
            );
        }

        [HttpPut("{id:required}")]
        public async Task<IActionResult> Update(int id, ModelRequest request)
        {
            var response = await service.Update(id, request).ConfigureAwait(false);

            if (response.Failed())
            {
                return InternalServerError(response);
            }

            return response.NotFound() ? NotFound(new { id }) : Ok(response);
        }

        [HttpDelete("{id:required}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContentResponse(await service.Remove(id).ConfigureAwait(false));
        }
    }
}
