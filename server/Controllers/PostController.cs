using Microsoft.AspNetCore.Mvc;
using server.DataAccess.Entities;
using server.Helper;
using server.Helper.Mapper;
using server.Services;
using server.ViewModel.Posts;

namespace server.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostLangService _postLangService;
        private readonly ILanguageService _languageService;
        public PostController(IPostService postService , IPostLangService postLangService , ILanguageService languageService)
        {
            _postService= postService;
            _postLangService= postLangService;
            _languageService= languageService;
        }


        [HttpGet]
        public async Task<ActionResult<PostModel>> GetAll([FromQuery] PostSearchModel model)
        {
            var data = await _postService.GetPagedPost(model);
            if (data == null)
            {
                return BadRequest("Wrong lang code ");
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails( [FromRoute] int id , [FromQuery] string? lang)
        {

            var post = await _postService.GetByIdAsync(id);

            if(post == null )
            {
                return NotFound("No post found with special id !");
            }
            else
            {
                if (!string.IsNullOrEmpty(lang))
                {
                    var language = await _languageService.GetByCode(lang);
                    if (language == null)
                    {
                        return BadRequest("No specific lang with the code");
                    }
                    else
                    {
                        var postLang = await _postLangService.GetBySpecificLang(id, language.Id);
                        if (postLang == null)
                        {
                            return Ok();
                        }
                        else
                            return Ok(Utilities.MapToPostModel(post, postLang));
                    }
                }
                else
                {
                    var result = new List<PostModel>();
                    result.Add(post.MapToModel());
                    var postLangs = await _postLangService.GetByPostId(id);
                    if(postLangs != null && postLangs.Any())
                    {
                        foreach(var postLang in postLangs)
                        {
                            var postModel = Utilities.MapToPostModel(post, postLang);
                            result.Add(postModel);
                        }
                    }
                    return Ok(result);
                }
            }          
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromBody] IReadOnlyList<PostModel> model)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound("No post found with special id !");
            }
            else
            {
                var data = await _postService.UpDateTransactional(model);
                return Ok(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<PostModel> model)
        {
            var data = await _postService.InsertTransactional(model);
            return Ok(data);         
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound("No post found with special id !");
            }
            else
            {
                var isSuccess = await _postService.DeleteTransactional(id);
                if (isSuccess)
                    return Ok();
                else 
                    return BadRequest();
            }
        }

    }
}
