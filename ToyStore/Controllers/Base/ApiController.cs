using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ToyStore.Controllers.Base
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ExceptionResult(Exception exception)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, $"Smtg went wrong: {exception.Message}" +
                                                                Environment.NewLine +
                                                                $"{exception?.InnerException?.Message}");
        }

    }
}
