using Dapper;
using MySqlConnector;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Users;
using BC = BCrypt.Net.BCrypt;
namespace server.DataAccess.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserByIdentify(LoginModel model );
        Task<bool> AlreadyExist(RegisterModel model);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public UserRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }

        public async Task<User> GetUserByIdentify(LoginModel model)
        {

            var query = "SELECT * FROM Users WHERE UserName = @Identify OR Email =@Identify ";

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { Identify = model.Identify });
                var isValid = BC.Verify(model.Password, result.Password);
                if (!isValid)
                {
                    return null;
                }               
                return result;
            }


        }
        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<User>(query, new {Id = id});
                return result;
            }
        }

        public  async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(query);
                return result.ToList();
            }
        }

        public async Task<User> AddAsync(User entity)
        {
            var sql = "INSERT INTO Users (UserName , Password , Email , IsAdmin ) VALUES (@UserName, @Password, @Email, @IsAdmin) ; SELECT LAST_INSERT_ID() ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql);
                entity.Id = result;
                return entity;
            }
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<bool> AlreadyExist(RegisterModel model)
        {
            var users =  await GetAllAsync();
            return users.Any(x => x.UserName == model.UserName || x.Email == model.Email);
        }
    }
}
