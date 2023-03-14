using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IPostLangService : IGenericService<PostLang>
    {

    }
    public class PostLangService :  IPostLangService
    {
        private readonly IUnitOfWork _unit;
        public PostLangService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public Task<PostLang> AddAsync(PostLang entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<PostLang>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostLang> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostLang> UpdateAsync(PostLang entity)
        {
            throw new NotImplementedException();
        }
    }
}
