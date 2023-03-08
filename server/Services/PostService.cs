using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IPostService : IEntityService<Post>
    {

    }
    public class PostService : EntityService<Post>, IPostService
    {
        private readonly IPostRepository _repos;
        public PostService(IUnitOfWork unitOfWork, IPostRepository repository) : base(unitOfWork, repository)
        {
            _repos = repository;
        }
    }
}
