using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
    }
    public class PostRepository : BaseRepository<Post> , IPostRepository
    {
        public PostRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
