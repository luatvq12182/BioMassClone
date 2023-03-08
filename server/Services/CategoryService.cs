using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface ICategoryService : IEntityService<Category>
    {

    }
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _repos;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository repository) : base(unitOfWork, repository)
        {
            _repos = repository;
        }
    }
}
