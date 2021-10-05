using System.Threading.Tasks;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Models.Results;
using CarManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    public class TypeController : BaseApiController
    {
        private readonly ITypeService service;

        public TypeController(ITypeService service) => this.service = service;

        [Authorize(Roles = AppUserRoles.AdminOrCanRead)]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50
        )
        {
            return OkResponse(await service.GetAll(pageNumber, pageSize).ConfigureAwait(false));
        }

        [Authorize(Roles = AppUserRoles.AdminOrCanRead)]
        [HttpGet("{id:required}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await service.GetById(id).ConfigureAwait(false);

            if (result.Failed())
            {
                return InternalServerError(result);
            }

            return result.NotFound() ? NotFound(new { id }) : Ok(result);
        }

        [Authorize(Roles = AppUserRoles.AdminOrCanAdd)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeRequest request)
        {
            var result = await service.Add(request).ConfigureAwait(false);

            if (result is not Success success)
            {
                return InternalServerError(result);
            }

            var type = (TypeResponse)success.Response;

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { controller = "Type", id = type.Id },
                value: type
            );
        }

        [Authorize(Roles = AppUserRoles.AdminOrCanUpdate)]
        [HttpPut("{id:required}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] TypeRequest request
        )
        {
            var result = await service.Update(id, request).ConfigureAwait(false);

            if (result.Failed())
            {
                return InternalServerError(result);
            }

            return result.NotFound() ? NotFound(new { id }) : Ok(result);
        }

        [Authorize(Roles = AppUserRoles.AdminOrCanDelete)]
        [HttpDelete("{id:required}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NoContentResponse(await service.Remove(id).ConfigureAwait(false));
        }
    }
}
