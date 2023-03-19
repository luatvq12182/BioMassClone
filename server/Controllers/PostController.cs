using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using server.Services;
using server.ViewModel.Posts;

namespace server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<ActionResult<PostModel>> GetAll([FromRoute]PostSearchModel model) 
        {
            var data = await _postService.GetPagedPost(model);
            return Ok(data);
        }
    }
}
