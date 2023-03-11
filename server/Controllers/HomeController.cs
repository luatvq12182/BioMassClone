using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Home()
        {
            return Ok("Server is running ! ");
        }
    }
}
