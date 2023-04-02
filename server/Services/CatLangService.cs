using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Categories;

namespace server.Services
{
    public interface ICatLangService : IGenericService<CatLang>
    {
        Task<IReadOnlyList<CategoryModel>> GetByCategoryId(int id);
    }
    public class CatLangService :  ICatLangService
    {
        private readonly IUnitOfWork _unit;
        public CatLangService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public Task<CatLang> AddAsync(CatLang entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CatLang>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CategoryModel>> GetByCategoryId(int id)
        {
            return await _unit.CatLang.GetByCategoryId(id);
        }

        public Task<CatLang> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CatLang> UpdateAsync(CatLang entity)
        {
            throw new NotImplementedException();
        }
    }
}
