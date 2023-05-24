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
            switch (exception?.Message)
            {
                case var message when message!.Contains("exist."):
                    return Problem(title: exception?.Message, statusCode: 404);
                case "Successfully.":
                    return Problem(title: exception?.Message, statusCode: 200);
                case "Sorry, there is someone before you booked this.":
                    return Problem(title: exception?.Message, statusCode: 409);
            }
            return Problem(title: exception?.Message, statusCode: 400);
        }
    }
}