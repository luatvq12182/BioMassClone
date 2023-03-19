using Dapper;
using Microsoft.Data.SqlClient;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Languages;

namespace server.DataAccess.Repositories
{
    public interface ILanguageRepository : IGenericRepository<Language>
    {
        public bool AlreadyExist(LanguageModel model, out string message);
        public Task<Language> GetByCode(string code);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public LanguageRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }

        public async Task<Language> AddAsync(Language entity)
        {
            var sql = "INSERT INTO Languages (Code, Name, IsDefault) VALUES (@Code,@Name,@IsDefault); SELECT CAST(SCOPE_IDENTITY() as int) ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public bool AlreadyExist(LanguageModel model, out string message)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = " DELETE  FROM Languages WHERE Id = @Id ";


            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Language>> GetAllAsync()
        {
            var query = " SELECT * FROM Languages ";


            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Language>(query);
                return result.ToList();
            }
        }

        public async Task<Language> GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            var query = "Select * FROM Languages WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Language>(query, new { Id = id });
                return result;
            }
        }

        public async Task<Language> UpdateAsync(Language entity)
        {
            var sql = "UPDATE Languages SET Code = @Code, Name = @Name, IsDefault=@Isdefault WHERE Id= @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                if (result > 0)
                {
                    return entity;
                }
                return null;
            }
        }
    }
}
