using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface ILanguageRepository : IBaseRepository<Language>
    {
    }
    public class LanguageRepository : BaseRepository<Language> , ILanguageRepository
    {
        public LanguageRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
