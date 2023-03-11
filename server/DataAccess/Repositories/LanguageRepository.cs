using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;
using server.ViewModel.Languages;

namespace server.DataAccess.Repositories
{
    public interface ILanguageRepository : IBaseRepository<Language>
    {
        public bool AlreadyExist(LanguageModel model, out string message);
    }
    public class LanguageRepository : BaseRepository<Language> , ILanguageRepository
    {
        public LanguageRepository(GreenWayDbContext context) :base(context) 
        {

        }

        public bool AlreadyExist(LanguageModel model, out string message)
        {
            message= string.Empty;
            var query = Dbset.AsQueryable();

            if(query.Any(x=>x.Code == model.Code))
            {
                message += " Language code already exist !";
            }
            if (query.Any(x => x.Name == model.Name))
            {
                message += " Language name already exist !";
            }
            if (string.IsNullOrEmpty(message))
            {
                return true;
            }
            return false;
        }
    }
}
