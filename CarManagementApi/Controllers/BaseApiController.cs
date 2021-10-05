using System;
using System.Net;
using CarManagementApi.Models.Results;
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

        protected IActionResult OkResponse(IResult result)
        {
            return ValidateResponse(Ok, result);
        }

        protected IActionResult NoContentResponse(IResult result)
        {
            return ValidateResponse(NoContent, result);
        }

        private IActionResult ValidateResponse(
            Func<IResult, ObjectResult> whenSuccess,
            IResult result
        )
        {
            return result.Succeeded() ? whenSuccess(result) : InternalServerError(result);
        }

        private ObjectResult NoContent(object value)
        {
            return StatusCode((int)HttpStatusCode.NoContent, value);
        }
    }
}
