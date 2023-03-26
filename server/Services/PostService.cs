using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.Helper;
using server.Helper.Mapper;
using server.ViewModel.Commons;
using server.ViewModel.Posts;

namespace server.Services
{
    public interface IPostService : IGenericService<Post>
    {
        public Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model);
        public Task<IReadOnlyList<PostModel>> InsertTransactional(IReadOnlyList<PostModel> model);
    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unit;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unit= unitOfWork;
        }

        public Task<Post> AddAsync(Post entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }
        public async Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model)
        {
            if(!string.IsNullOrEmpty(model.Lang))
            {
                var language = await _unit.Language.GetByCode(model.Lang);
                if(language != null)
                {
                    List<PostModel> Items = new List<PostModel>();
                    int TotalCount;
                    var postLangs = await _unit.PostLang.GetAllBySpecificLang(language.Id);
                    var posts = await _unit.Post.GetAllAsync();
                    if(posts!= null && posts.Any())
                    {
                        foreach(var post in posts)
                        {
                            if(postLangs != null && postLangs.Any())
                            {
                                foreach(var postLang in postLangs)
                                {
                                    if(postLang.PostId == post.Id)
                                    {
                                        var postModel = Utilities.MapToPostModel(post, postLang);
                                        Items.Add(postModel);
                                    }
                                }
                            }
                        }
                    }
                    TotalCount = Items.Count;
                    return new PaginatedList<PostModel>(Items,TotalCount,model.PageNumber,model.Pagesize);
                }

            }
            return await _unit.Post.GetPagedPost(model);
        }

        public async Task<IReadOnlyList<PostModel>> InsertTransactional(IReadOnlyList<PostModel> model)
        {
            var standardItem = model.FirstOrDefault(x => x.LanguageId is null);
            if (standardItem != null)
            {
                var insertedPost = await _unit.Post.AddTransactionalAsync(standardItem.MapToEntity());
                var spescificItems = model.Where(x => x.LanguageId > 0).ToList();
                if (spescificItems != null && spescificItems.Any())
                {
                    foreach (var item in spescificItems)
                    {
                        await _unit.PostLang.AddTransactionalAsync(new PostLang { PostId = insertedPost.Id, LangId = item.LanguageId.Value, Title = item.Title, Body = item.Body, ShortDescription = item.ShortDescription });
                    }
                }
                if (await _unit.CommitThings())
                {
                    return model;
                }
            }
            return null;
        }
    }
}
