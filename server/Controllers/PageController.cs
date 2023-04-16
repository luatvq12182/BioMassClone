using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [ApiController]
    [Route("api/pages")]
    public class PageController  : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("about-us")]
        public ActionResult<string> AboutUs()
        {
            var fileName = "AboutUs.html";
            var pageDirectory = Path.Combine("Pages",fileName);
            var filePath = Path.Combine(_env.WebRootPath, pageDirectory);
            var fileContent = System.IO.File.ReadAllText(filePath);

            return fileContent;
        }
    }
}
