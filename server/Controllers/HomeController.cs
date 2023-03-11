using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Home()
        {
            return Ok("Server is running ! ");
        }
    }
}
