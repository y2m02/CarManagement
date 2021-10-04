using System;
using System.Net;
using CarManagementApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult OkResponse(IResponse response)
        {
            return ValidateResponse(Ok, response);
        }

        protected IActionResult NoContentResponse(IResponse response)
        {
            return ValidateResponse(NoContent, response);
        }

        private IActionResult ValidateResponse(
            Func<IResponse, ObjectResult> whenSuccess,
            IResponse response
        )
        {
            return response.Succeeded() ? whenSuccess(response) : InternalServerError(response);
        }

        private ObjectResult NoContent(object value)
        {
            return StatusCode((int)HttpStatusCode.NoContent, value);
        }
    }
}
