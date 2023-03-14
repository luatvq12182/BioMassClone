using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IPostService : IGenericService<Post>
    {

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
    }
}
