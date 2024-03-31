using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/Handler/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode = 500)
        {
            _logger.LogInformation($"[HttpStatusCodeHandler] An error occurred! Error code: [{statusCode}]");
            ViewBag.ErrorCode = statusCode;

			ViewBag.ErrorMessage = statusCode switch
			{
				404 => "Page not found!",
				_ => "Sorry an error occurred! We are trying to solve it as soon as possible, please try again later!",
			};
			return View("Error");
        }
    }
}