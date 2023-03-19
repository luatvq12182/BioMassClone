using Dapper;
using Microsoft.Data.SqlClient;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Categories;

namespace server.DataAccess.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> AddTransactionalAsync(Category entity);
        public Task<int> DeleteTransactionalAsync(int id);
        public Task<IReadOnlyList<CategoryModel>> GetAll();
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public CategoryRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }
        public async Task<Category> AddAsync(Category entity)
        {
            var sql = "INSERT INTO Categories (Slug,Name) VALUES(@Slug,@Name) ; SELECT CAST(SCOPE_IDENTITY() as int) ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public async Task<Category> AddTransactionalAsync(Category entity)
        {
            var sql = "INSERT INTO Categories (Slug,Name) VALUES(@Slug,@Name) ; SELECT CAST(SCOPE_IDENTITY() as int) ";

            var result = await _session.Connection.QuerySingleAsync<int>(sql, entity, _session.Transaction);
            entity.Id = result;
            return entity;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Categories WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteTransactionalAsync(int id)
        {
            var sql = "DELETE FROM Categories WHERE Id = @Id";

            var result = await _session.Connection.ExecuteAsync(sql, new { Id = id }, _session.Transaction);
            return result;

        }

        public async Task<IReadOnlyList<CategoryModel>> GetAll()
        {
            var query = "SELECT * FROM Categories ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CategoryModel>(query);
                return result.ToList();
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Categories WHERE Id = @Id ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id });
                return result;
            }
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            var sql = "UPDATE Categories  SET Slug=@Slug , Name = @Name WHERE Id = @Id ";

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

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            var query = "SELECT * FROM Categories ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(query);
                return result.ToList();
            }
        }
    }
}
