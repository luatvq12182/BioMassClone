using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.ViewModel.Categories;
using server.DataAccess.Entities;

namespace server.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/categories")]
    public class CategoryControler : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryControler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("categories")]
        public async Task< IActionResult> GetAll(int? languageId) 
        {
            var data =  await _categoryService.GetByLanguageId(languageId);
            return Ok(data);
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _categoryService.GetDetails(id);
            return Ok(data);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> Add(IReadOnlyList<CategoryModel> model)
        {
            var data = await _categoryService.InsertCategoryTransactional(model);
            return Ok(data);
        }

        [HttpDelete("categories{id}")]
        public async Task<IActionResult> Delete( [FromRoute] int id )
        {
            if(await _categoryService.Remove(id))
                return Ok();
            else 
                return NotFound();
        }

        [HttpPut("categories{id}")]
        public async Task<IActionResult> Update([FromRoute] int id ,IReadOnlyList<CategoryModel> model)
        {
            var entity = await _categoryService.GetById(id);
            if(entity == null)
                return NotFound();
            else
            {


                return Ok();
            }
        }
    }
}
