using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.ViewModel.Languages;


namespace server.Services
{
    public interface ILanguageService : IGenericService<Language>
    {
        public Language GetByCode(string code);
        public bool AlreadyExist(LanguageModel language, out string message);
    }
    public class LanguageService : ILanguageService
    {

        private readonly IUnitOfWork _unit;
        public LanguageService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public async Task<Language> AddAsync(Language entity)
        {
            return await _unit.Language.AddAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _unit.Language.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Language>> GetAllAsync()
        {
            return await _unit.Language.GetAllAsync(); 
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _unit.Language.GetByIdAsync(id);
        }

        public async Task<Language> UpdateAsync(Language entity)
        {
            return await _unit.Language.UpdateAsync(entity);
        }
        public bool AlreadyExist(LanguageModel language, out string message)
        {
            return _unit.Language.AlreadyExist(language, out message);
        }

        public async Task<Language> GetByCode(string code)
        {
            return await _unit.Language.GetByCode(code);
        }

        Language ILanguageService.GetByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
