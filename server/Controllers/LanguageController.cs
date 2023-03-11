using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.DataAccess.Entities;
using server.ViewModel.Languages;

namespace server.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpGet("languages")]
        public IActionResult GetAll()
        {
            var data = _languageService.GetAll();
            return Ok(data);
        }
        [HttpGet("languages/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _languageService.GetById(id);
            return Ok(data);
        }

        [HttpPost("languages")]
        public async Task<IActionResult> Add(LanguageModel model)
        {
            var data = await _languageService.Insert(new Language 
            {
                Name = model.Name,
                Code= model.Code,
                IsDefault =model.IsDefault
            });
            return Ok(data);
        }
        [HttpPut("languages/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id , LanguageModel model)
        {
            var entity = await _languageService.GetById(id);
            if(entity == null)
            {
                return BadRequest("Not found !");
            }

            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.IsDefault = model.IsDefault;
            await _languageService.Update(entity);            
            return Ok();
        }

        [HttpDelete("languages/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _languageService.GetById(id);
            if (entity == null)
            {
                return BadRequest("Not found !");
            }
            if (_languageService.Delete(id))
                return Ok();
            else 
                return BadRequest();
        }
    }
}
