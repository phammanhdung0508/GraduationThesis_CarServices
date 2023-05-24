using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [Route("/error")]
        // [HttpGet]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            switch (exception)
            {
                case NullReferenceException:
                    return Problem(title: exception?.Message, statusCode: 404);
                case ArgumentOutOfRangeException:
                    return Problem(title: exception?.Message, statusCode: 404);
                case ArgumentException:
                    return Problem(title: exception?.Message, statusCode: 404);
                case TaskCanceledException:
                    return Problem(title: exception?.Message, statusCode: 409);
                default:
                    return Problem(title: exception?.Message, statusCode: 500);
            }
        }
    }
}