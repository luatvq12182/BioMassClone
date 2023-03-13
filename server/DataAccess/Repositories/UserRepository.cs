using Microsoft.EntityFrameworkCore;
using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;
using server.ViewModel.Users;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using BC = BCrypt.Net.BCrypt;
namespace server.DataAccess.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetUserByIdentify(LoginModel model );
        bool AlreadyExist(RegisterModel model , out string message);
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GreenWayDbContext context) : base(context) 
        {

        }

        public async Task<User> GetUserByIdentify(LoginModel model)
        {
            var passWordHash = BC.HashPassword(model.Password);        
            var isValid = BC.Verify(model.Password, passWordHash);

            var result =  await Dbset.FirstOrDefaultAsync(x=>x.UserName== model.Identify && isValid || x.Email == model.Identify && isValid);
            return result;
        }

        public bool AlreadyExist(RegisterModel model, out string message)
        {
            message = string.Empty;
            var query = Dbset.AsQueryable().ToList();
            if(query.Any(x=>x.UserName == model.UserName))
            {
                message += " User name already exist !";
            }
            if (query.Any(x => x.UserName == model.UserName))
            {
                message += " Email already exist !";
            }
            if (!string.IsNullOrEmpty(message))
            {
                return true;
            }
            return false;
        }
    }
}
