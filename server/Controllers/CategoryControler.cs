using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Helper.Mapper;
using server.Services;
using server.ViewModel.Categories;

namespace server.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryControler : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        private readonly ICatLangService _catLangService;
        public CategoryControler(ICategoryService categoryService , ILanguageService languageService, ICatLangService catLangService)
        {
            _categoryService = categoryService;
            _languageService = languageService;
            _catLangService = catLangService;
        }
        [HttpGet]
        public async Task< IActionResult> GetAll([FromQuery]string? lang) 
        {
            int? languageId = null;
            if (!string.IsNullOrEmpty(lang))
            {
                var language = await _languageService.GetByCode(lang);
                languageId = language != null ? language.Id : null;
            }
            var data =  await _categoryService.GetByLanguageId(languageId);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _categoryService.GetDetails(id);
            return Ok(data);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(IReadOnlyList<CategoryModel> model)
        {
            var data = await _categoryService.InsertCategoryTransactional(model);
            return Ok(data);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( [FromRoute] int id )
        {
            if(await _categoryService.Remove(id))
                return Ok();
            else 
                return NotFound();
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id ,IReadOnlyList<CategoryModel> model)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if(entity == null)
                return NotFound();
            else
            {
                var data = await _categoryService.UpdateTransactionalAsync(model);
                return Ok(data);

            }
        }
    }
}
