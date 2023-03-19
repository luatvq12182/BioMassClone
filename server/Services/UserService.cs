using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.ViewModel.Users;

namespace server.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetUserByIdentify(LoginModel model);
        Task<bool> AlreadyExist(RegisterModel model);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _unit.User.AddAsync(entity);
        }

        public async Task<bool> AlreadyExist(RegisterModel model)
        {
            return await _unit.User.AlreadyExist(model);
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdentify(LoginModel model)
        {
            return await _unit.User.GetUserByIdentify(model);
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
