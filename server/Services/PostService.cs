using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.Helper;
using server.Helper.Mapper;
using server.ViewModel.Commons;
using server.ViewModel.Posts;

namespace server.Services
{
    public interface IPostService : IGenericService<Post>
    {
        Task<PaginatedList<PostViewModel>> GetPagedPost(PostSearchModel model);
        Task<PostModel> InsertTransactional(PostModel model);
        Task<PostModel> UpDateTransactional(PostModel model);
        Task<bool> DeleteTransactional(int id);
    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unit;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
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

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _unit.Post.GetByIdAsync(id);
        }

        public Task<Post> UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }
        public async Task<PaginatedList<PostViewModel>> GetPagedPost(PostSearchModel model)
        {
            List<PostViewModel> Items = new List<PostViewModel>();
            int TotalCount;

            var language = await _unit.Language.GetByCode(model.Lang);
            if (language != null)
            {
                var posts = await _unit.Post.GetAllAsync();

                if (model.IsShowOnHomePage.HasValue && model.IsShowOnHomePage.Value)
                {
                    posts = posts.Where(x=>x.ShowOnHomePage).ToList();

                    foreach(var post in posts)
                    {
                        var postLang = await _unit.PostLang.GetBySpecificLang(post.Id,language.Id);
                        if (postLang != null)
                        {
                            var postViewModel = Utilities.MapToPostModel(post, postLang);
                            Items.Add(postViewModel);
                        }
                    }
                }
                else
                {
                    foreach (var post in posts)
                    {
                        var postLang = await _unit.PostLang.GetBySpecificLang(post.Id, language.Id);
                        if(postLang != null)
                        {
                            var postViewModel = Utilities.MapToPostModel(post, postLang);
                            Items.Add(postViewModel);
                        }
                    }
                }
                TotalCount = Items.Count;
                Items = Items.OrderByDescending(p => p.CreatedDate).ToList();
                return new PaginatedList<PostViewModel>(Items, TotalCount, model.PageNumber, model.Pagesize);
            }
            else { return null; }

        }

        public async Task<PostModel> InsertTransactional(PostModel model)
        {
            var post = new Post { Author = model.Author, CategoryId = model.CategoryId, CreatedDate = DateTime.Now, ShowOnHomePage = model.IsShowOnHomePage, Thumbnail = model.Thumbnail, Views = 0 };

            var insertedPost = await _unit.Post.AddTransactionalAsync(post);
            if (model.Items != null && model.Items.Any())
            {
                foreach (var item in model.Items)
                {
                    item.PostId = insertedPost.Id;
                    await _unit.PostLang.AddTransactionalAsync(item);
                }
            }
            if (await _unit.CommitThings())
            {
                return model;
            }
            else
                return null;
        }

        public async Task<PostModel> UpDateTransactional(PostModel model)
        {
            var post = new Post { Id = model.Id, Author = model.Author, CategoryId = model.CategoryId, ShowOnHomePage = model.IsShowOnHomePage, Thumbnail = model.Thumbnail };

            await _unit.Post.UpdateTransactionalAsync(post);

            if (model.Items != null && model.Items.Any())
            {
                foreach (var item in model.Items)
                {
                    await _unit.PostLang.UpdateTransactional(item);
                }
            }
            if (await _unit.CommitThings())
            {
                return model;
            }
            else
                return null;
        }

        public async Task<bool> DeleteTransactional(int id)
        {
            var result = false;
            await _unit.PostLang.DeleteTransactionalAsync(id);
            await _unit.Post.DeleteTransactionalAsync(id);
            if (await _unit.CommitThings())
            {
                result = true;
            }
            return result;
        }
    }
}
