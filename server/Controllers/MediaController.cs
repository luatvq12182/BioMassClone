using Microsoft.AspNetCore.Mvc;
using server.Services;
using System.Net.Http.Headers;

namespace server.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/media")]
    public class MediaController :ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;
        public MediaController(IImageService imageService , IWebHostEnvironment env)
        {
            _imageService= imageService;
            _env = env;
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if(file != null)
            {

                string fileExtenstion = Path.GetExtension(file.FileName).Trim();

            }
            try
            {
                var mediaDirectory = Path.Combine("Uploads", "Images");
                string path = Path.Combine(_env.WebRootPath, mediaDirectory);

                var media = Request.Form.Files[0];
                if (media.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(media.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(path, fileName);
                    var dbPath = Path.Combine(mediaDirectory, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        media.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
