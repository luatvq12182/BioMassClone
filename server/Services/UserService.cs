using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IUserService : IEntityService<User>
    {
       Task<User> GetUserByUserName(string userName);
    }
    public class UserService : EntityService<User> , IUserService
    {
        private readonly IUserRepository _repos;
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork, repository)
        {
            _repos = repository;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var result =  await _repos.GetUserByUserName(userName);
            return result;

        }
    }
}
