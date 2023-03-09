using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok("ngon chim");
        }
    }
}
