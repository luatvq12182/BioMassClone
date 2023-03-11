using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Languages;

namespace server.Services
{
    public interface ILanguageService : IEntityService<Language>
    {
        public Language GetByCode(string code);
        public bool AlreadyExist (LanguageModel language , out string message);
    }
    public class LanguageService : EntityService<Language>, ILanguageService
    {
        private readonly ILanguageRepository _repos;
        public LanguageService(IUnitOfWork unitOfWork , ILanguageRepository repository) : base(unitOfWork,repository)
        {
            _repos= repository;
        }

        public bool AlreadyExist(LanguageModel language, out string message)
        {
            return _repos.AlreadyExist(language, out message);
        }

        public  Language GetByCode(string code)
        {
            var result =  _repos.Find(x=>x.Code == code);
            return result;
        }
    }
}
