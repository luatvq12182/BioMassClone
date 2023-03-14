using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.DataAccess.Entities;
using server.ViewModel.Languages;
using MySqlConnector;

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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _languageService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _languageService.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LanguageModel model)
        {
            //if (_languageService.AlreadyExist(model, out string message))
            //{
            //    return BadRequest(message);
            //}
            var data = await _languageService.AddAsync(new Language 
            {
                Name = model.Name,
                Code= model.Code,
                IsDefault =model.IsDefault
            });
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id , LanguageModel model)
        {
            var entity = await _languageService.GetByIdAsync(id);
            if(entity == null)
            {
                return BadRequest("Not found !");
            }
            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.IsDefault = model.IsDefault;
            await _languageService.UpdateAsync(entity);            
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _languageService.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("Not found !");
            }
            await _languageService.DeleteAsync(id);
            return Ok();

        }
    }
}
