using System.Threading.Tasks;
using CarManagementApi.Models.Requests;
using CarManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    public class AppUserController : BaseApiController
    {
        private readonly IAppUserService service;

        public AppUserController(IAppUserService service) => this.service = service;

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAppUserRequest request)
        {
            var result = await service.Register(request).ConfigureAwait(false);

            return result.HasValidation() ? BadRequest(result) : OkResponse(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await service.SignIn(request).ConfigureAwait(false);

            return result.Unauthorized() ? Unauthorized(result) : OkResponse(result);
        }

        [HttpPost("AddToRoles")]
        [Authorize]
        public async Task<IActionResult> AddToRoles([FromBody] AddAppUserToRolesRequest request)
        {
            var result = await service.AddToRoles(request).ConfigureAwait(false);

            return result.HasValidation() ? BadRequest(result) : OkResponse(result);
        }
    }
}
