using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IPostLangService : IGenericService<PostLang>
    {
        Task<PostLang> GetBySpecificLang(int postId, int langId);
        Task<IReadOnlyList<PostLang>> GetByPostId(int id);
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

        public async Task<IReadOnlyList<PostLang>> GetByPostId(int id)
        {
            return await _unit.PostLang.GetPostId(id);
        }

        public  async Task<PostLang> GetBySpecificLang(int postId, int langId)
        {
            return await _unit.PostLang.GetBySpecificLang(postId, langId);
        }

        public Task<PostLang> UpdateAsync(PostLang entity)
        {
            throw new NotImplementedException();
        }
    }
}
