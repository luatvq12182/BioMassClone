using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Commons;
using server.ViewModel.Posts;

namespace server.Services
{
    public interface IPostService : IGenericService<Post>
    {
        public Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model);
    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unit;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unit= unitOfWork;
        }

        public async Task<Post> AddAsync(Post entity)
        {
            return await _unit.Post.AddAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await (_unit.Post.DeleteAsync(id));
        }

        public async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            return await _unit.Post.GetAllAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _unit.Post.GetByIdAsync(id);
        }

        public async Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model)
        {
            return await _unit.Post.GetPagedPost(model);
        }

        public Task<Post> UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
