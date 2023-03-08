using Microsoft.EntityFrameworkCore;
using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace server.DataAccess.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetUserByUserName(string userName);
        Task<bool> UserNameAlreadyExist(string userName);
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GreenWayDbContext context) : base(context) 
        {

        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var result =  await Dbset.FirstOrDefaultAsync(x=>x.UserName== userName);
            return result;
        }

        public async Task<bool> UserNameAlreadyExist(string userName)
        {
            return await Dbset.AnyAsync(x=>x.UserName== userName);
        }
    }
}
