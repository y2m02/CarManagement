using System.Threading.Tasks;
using CarManagementApi.Models.Requests;
using CarManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    public class AppUserController : BaseApiController
    {
        private readonly IAppUserService service;

        public AppUserController(IAppUserService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAppUserRequest request)
        {
            var result = await service.Register(request).ConfigureAwait(false);

            return result.HasValidation() ? BadRequest(result) : OkResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AddToRolesRequest request)
        {
            var result = await service.AddToRoles(request).ConfigureAwait(false);

            return result.HasValidation() ? BadRequest(result) : OkResponse(result);
        }
    }
}
