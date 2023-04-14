using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Helper;
using System.IO;
using server.DataAccess.Entities;

namespace server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/media")]
    public class MediaController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;
        public MediaController(IImageService imageService, IWebHostEnvironment env)
        {
            _imageService = imageService;
            _env = env;
        }
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var filesToUpload = HttpContext.Request.Form.Files;
            if (filesToUpload != null && filesToUpload.Any())
            {
                foreach (var file in filesToUpload)
                {
                    try
                    {
                        var mediaDirectory = Path.Combine("Uploads", "Images");
                        string filePath = Path.Combine(_env.WebRootPath, mediaDirectory);

                        string fileExtension = Path.GetExtension(file.FileName).Trim();
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var friendlyName = Utilities.SEOUrl(fileName);

                        string saveFileName = friendlyName + fileExtension;

                        var isSuccess = await Utilities.UploadFile(file, filePath, saveFileName);
                        if (isSuccess)
                        {
                            var pathToSave = Utilities.STATIC_IMAGE_PATH + saveFileName;
                            var image = new Image
                            {
                                Name = saveFileName,
                                Slug = friendlyName,
                                ImageUrl = pathToSave
                            };
                            await _imageService.AddAsync(image);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            var data = await _imageService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("slider-images")]
        public async Task<IActionResult> GetSliderImages()
        {
            var data = await _imageService.GetAllAsync();
            data = data.Where(x => x.ShowOnSlider == true).ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _imageService.GetByIdAsync(id);
            return Ok(data);
        }
        [HttpPost("add-to-slider")]
        public async Task<IActionResult> AddToSlider([FromBody] List<int> ids)
        {
            if(await _imageService.AddToSlider(ids))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("remove-from-slider/{id}")]
        public async Task<IActionResult> RemoveFromSlider(int id)
        {
            var image =  await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound(); 
            else
            {
                if (await _imageService.RemoveFromSlider(id))
                {
                    return Ok();
                }else
                {
                    return BadRequest();
                }
            }
        }
        [HttpGet("is-already-in-use")]
        public async Task<IActionResult> AlreadyInUse(string imageUrl)
        {
            var data = await _imageService.AlreadyInUse(imageUrl);
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();
            else
            {
                if (await _imageService.DeleteAsync(id) > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
