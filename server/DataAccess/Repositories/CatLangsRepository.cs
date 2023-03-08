using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface ICatLangsRepository : IBaseRepository<CatLang>
    {
    }
    public class CatLangRepository : BaseRepository<CatLang> , ICatLangsRepository
    {
        public CatLangRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
