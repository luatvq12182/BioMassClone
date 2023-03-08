using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface IPostLangRepository : IBaseRepository<PostLang>
    {
    }
    public class PostLangRepository : BaseRepository<PostLang> , IPostLangRepository
    {
        public PostLangRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
