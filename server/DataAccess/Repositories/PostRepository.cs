using Dapper;
using MySqlConnector;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Commons;
using server.ViewModel.Posts;


namespace server.DataAccess.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task<Post> AddTransactionalAsync(Post post);
        public Task<Post> UpdateTransactionalAsync(Post post);
        public Task<bool> DeleteTransactionalAsync(int id);
    }
    public class PostRepository : IPostRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public PostRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }
        public async Task<Post> AddAsync(Post entity)
        {
            var sql = "INSERT INTO Posts (CategoryId,CreatedDate , Views , Author , ShowOnHomePage) VALUES (@CategoryId, @CreatedDate, @Views, @Author,@ShowOnHomePage) ; SELECT LAST_INSERT_ID() ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Posts WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return result;
            }
        }

        public async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            var query = "SELECT * FROM Posts ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Post>(query);
                return result.ToList();
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Posts WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Post>(query, new { Id = id });
                return result;
            }
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            var sql = "UPDATE Posts SET CategoryId = @CategoryId ,Views = @Views , Author=@Author , ShowOnHomePage = @ShowOnHomePage WHERE Id=@Id ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
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
        public async Task<Post> AddTransactionalAsync(Post entity)
        {
            var sql = "INSERT INTO Posts (CategoryId , CreatedDate , Views , Author , ShowOnHomePage) VALUES (@CategoryId,  @CreatedDate, @Views, @Author , @ShowOnHomePage) ; SELECT LAST_INSERT_ID() ";

            var result = await _session.Connection.QueryFirstOrDefaultAsync<int>(sql, entity, _session.Transaction);
            entity.Id = result;
            return entity;
        }

        public async Task<Post> UpdateTransactionalAsync(Post post)
        {
            var sql = "UPDATE Posts SET CategoryId = @CategoryId ,Views = @Views , Author=@Author ,Thumbnail= @Thumbnail , ShowOnHomePage = @ShowOnHomePage WHERE Id=@Id ";
            var result = await _session.Connection.ExecuteAsync(sql, post, _session.Transaction);
            if (result > 0)
            {
                return post;
            }
            return null;
        }

        public async Task<bool> DeleteTransactionalAsync(int id)
        {
            var sql = " DELETE FROM Posts WHERE Id = @Id ";
            var result = await _session.Connection.ExecuteAsync(sql, new { Id = id }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
