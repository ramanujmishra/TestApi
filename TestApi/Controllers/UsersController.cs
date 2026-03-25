using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [ValidateAntiForgeryToken] // for csrf protection, client must include valid antiforgery token in request header
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            if(!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data);
        }
    }
}
