using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
    public class CategoryRepository : BaseRepository<Category> , ICategoryRepository 
    {
        public CategoryRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
