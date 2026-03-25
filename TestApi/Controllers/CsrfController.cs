using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    public class CsrfController : ControllerBase
    {
        [HttpGet("csrf-token")]
        public IActionResult Index([FromServices] IAntiforgery antiforgery)
        {
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            return Ok(new { csrfToken= tokens.RequestToken }); // sets antiforgery token in response body,
                                                           // client can read and include in subsequent requests
        }
    }
}
