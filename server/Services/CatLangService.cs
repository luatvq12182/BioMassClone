using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface ICatLangService : IEntityService<CatLang>
    {

    }
    public class CatLangService : EntityService<CatLang> , ICatLangService
    {
        private readonly ICatLangsRepository _repos;
        public CatLangService(IUnitOfWork unitOfWork , ICatLangsRepository repository): base (unitOfWork,repository)
        {
            _repos= repository;
        }
    }
}
