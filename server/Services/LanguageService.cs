using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface ILanguageService : IEntityService<Language>
    {

    }
    public class LanguageService : EntityService<Language>, ILanguageService
    {
        private readonly ILanguageRepository _repos;
        public LanguageService(IUnitOfWork unitOfWork , ILanguageRepository repository) : base(unitOfWork,repository)
        {
            _repos= repository;
        }
    }
}
