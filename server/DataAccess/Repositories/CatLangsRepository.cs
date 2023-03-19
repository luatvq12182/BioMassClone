using Dapper;
using Microsoft.Data.SqlClient;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Categories;

namespace server.DataAccess.Repositories
{
    public interface ICatLangsRepository : IGenericRepository<CatLang>
    {
        public Task<IReadOnlyList<CategoryModel>> GetByCategoryId(int id);
        public Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int id);
        public Task<CatLang> AddTransactionalAsync(CatLang entity);
        public Task<CatLang> UpdateTransactionalAsync(CatLang entity);
    }
    public class CatLangRepository : ICatLangsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public CatLangRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }
        public async Task<CatLang> AddAsync(CatLang entity)
        {
            var sql = "INSERT INTO CatLangs (CategoryId, LanguageId, Slug, Name) VALUES (@CategoryId, @LanguageId, @Slug, @Name); SELECT CAST(SCOPE_IDENTITY() as int) ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public async Task<CatLang> AddTransactionalAsync(CatLang entity)
        {
            var sql = "INSERT INTO CatLangs (CategoryId, LanguageId, Slug, Name) VALUES (@CategoryId, @LanguageId, @Slug, @Name); SELECT CAST(SCOPE_IDENTITY() as int) ";

            var result = await _session.Connection.QuerySingleAsync<int>(sql, entity, _session.Transaction);
            entity.Id = result;
            return entity;

        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM CatLangs  WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }

        }

        public async Task<IReadOnlyList<CatLang>> GetAllAsync()
        {
            var query = " SELECT * FROM CatLangs ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CatLang>(query);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<CategoryModel>> GetByCategoryId(int id)
        {
            var query = "SELECT * FROM CatLangs WHERE CategoryId = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CategoryModel>(query, new { Id = id });
                return result.ToList();
            }
        }

        public async Task<CatLang> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM CatLangs WHERE Id = @Id ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<CatLang>(query, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int id)
        {
            var query = "SELECT * FROM CatLangs WHERE LanguageId = @LangId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CategoryModel>(query, new { LangId = id });
                return result.ToList();
            }

        }

        public async Task<CatLang> UpdateAsync(CatLang entity)
        {
            var sql = "UPDATE CatLangs SET LanguageId = @LanguageId , CategoryId = @CategoryId , Slug = @Slug , Name = @Name WHERE Id = @Id ";
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

        public async Task<CatLang> UpdateTransactionalAsync(CatLang entity)
        {
            var sql = "UPDATE CatLangs SET LanguageId = @LanguageId , CategoryId = @CategoryId , Slug = @Slug , Name = @Name WHERE Id = @Id ";

            var result = await _session.Connection.ExecuteAsync(sql, entity, _session.Transaction);
            if (result > 0)
            {
                return entity;
            }
            return null;
        }
    }
}
