using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.ViewModel.Categories;
using server.DataAccess.Entities;

namespace server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/categories")]
    public class CategoryControler : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryControler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("categories")]
        public IActionResult GetAll() 
        {
            var data = _categoryService.GetAll();
            return Ok(data);
        }
        [HttpGet("categories/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _categoryService.GetById(id);
            return Ok(data);
        }

        [HttpPost("categories")]
        public IActionResult Add(CategoryModel model)
        {
            var data = _categoryService.Insert(new Category
            {
                Slug= model.Slug
            });
            return Ok(data);
        }
    }
}
