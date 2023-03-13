using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Users;

namespace server.Services
{
    public interface IUserService : IEntityService<User>
    {
       Task<User> GetUserByIdentify(LoginModel model);
        bool AlreadyExist(RegisterModel model , out string message);
    }
    public class UserService : EntityService<User> , IUserService
    {
        private readonly IUserRepository _repos;
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork, repository)
        {
            _repos = repository;
        }

        public bool AlreadyExist(RegisterModel model , out string message)
        {
            return _repos.AlreadyExist(model, out message);
        }

        public async Task<User> GetUserByIdentify(LoginModel model)
        {
            var result =  await _repos.GetUserByIdentify(model);
            return result;

        }
    }
}
