using System.Threading.Tasks;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Models.Results;
using CarManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly IBrandService service;

        public BrandController(IBrandService service) => this.service = service;

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
        public async Task<IActionResult> Create(BrandRequest request)
        {
            var response = await service.Add(request).ConfigureAwait(false);

            if (response is not Success success)
            {
                return InternalServerError(response);
            }

            var brand = (BrandResponse)success.Response;

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { controller = "Brand", id = brand.Id },
                value: brand
            );
        }

        [HttpPut("{id:required}")]
        public async Task<IActionResult> Update(int id, BrandRequest request)
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
