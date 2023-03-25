using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
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
