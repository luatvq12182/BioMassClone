using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using Dapper;
using MySqlConnector;

namespace server.DataAccess.Repositories
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        public Task<bool> AddToSlider(int id);
    }
    public class ImageRepository : IImageRepository
    {

        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public ImageRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }

        public async Task<Image> AddAsync(Image entity)
        {
            var sql = " INSERT INTO Images (Name , SLug , ImageUrl , ShowOnSlider) VALUES (@Name, @Slug,@ImageUrl,@ShowOnSlider) ; SELECT LAST_INSERT_ID() ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public async Task<bool> AddToSlider(int id)
        {
            var sql = "UPDATE Images SET ShowOnSlider = 1 WHERE Id = @Id";
            var result =  await _session.Connection.ExecuteAsync(sql, new {Id = id});
            if(result > 0)
            {
                return true;

            }
            return false;

        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = " DELETE FROM Images WHERE Id = @Id ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new {Id = id});
                return result;
            }
        }

        public async Task<IReadOnlyList<Image>> GetAllAsync()
        {
            var query = "SELECT * FROM Images ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Image>(query);
                return result.ToList();
            }
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Images WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Image>(query, new { Id = id });
                return result;
            }
        }

        public async Task<Image> UpdateAsync(Image entity)
        {
            var sql = "UPDATE Images SET  Name = @Name , SLug = @Slug , ImageUrl = @ImageUrl , ShowOnSlider = @ShowOnSlider ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql,entity);
                if(result > 0)
                {
                    return entity;
                }
                return null;
            }
        }
    }
}
