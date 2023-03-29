using Dapper;
using MySqlConnector;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;

namespace server.DataAccess.Repositories
{
    public interface IPostLangRepository : IGenericRepository<PostLang>
    {
        public Task<PostLang> GetBySpecificLang(int postId, int languageId);
        public Task<IReadOnlyList<PostLang>> GetPostId(int postId);
        public Task<PostLang> AddTransactionalAsync(PostLang entity);
        public Task<IReadOnlyList<PostLang>> GetAllBySpecificLang(int languageId);
        public Task<PostLang> UpdateTransactional (PostLang entity);
    }
    public class PostLangRepository :IPostLangRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DbSession _session;
        public PostLangRepository(IConfiguration configuration, DbSession dbSession)
        {
            _configuration = configuration;
            _session = dbSession;
        }
        public async Task<PostLang> AddAsync(PostLang entity)
        {
            var sql = "INSERT INTO PostLangs (PostId, LanguageId , Title, Body ,ShortDescription) VALUES (@PostId,@LanguageId , @Title, @Body ,@ShortDescription) ; SELECT LAST_INSERT_ID() ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = result;
                return entity;
            }
        }

        public async Task<PostLang> AddTransactional(PostLang entity)
        {
            var sql = "INSERT INTO PostLangs (PostId, LanguageId , Title, Body ,ShortDescription) VALUES (@PostId,@LanguageId , @Title, @Body ,@ShortDescription) ; SELECT LAST_INSERT_ID() ";

                var result = await _session.Connection.QuerySingleAsync<int>(sql, entity, _session.Transaction);
                entity.Id = result;
                return entity;
            
        }

        public async Task<PostLang> AddTransactionalAsync(PostLang entity)
        {
            var sql = "INSERT INTO PostLangs (PostId, LanguageId , Title, Body ,ShortDescription) VALUES (@PostId,@LanguageId , @Title, @Body ,@ShortDescription) ; SELECT LAST_INSERT_ID() ";
            var result = await _session.Connection.QuerySingleAsync<int>(sql, entity, _session.Transaction);
            entity.Id = result;
            return entity;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM PostLangs WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return result;
            }
        }

        public async Task<IReadOnlyList<PostLang>> GetAllAsync()
        {
            var query = "SELECT * FROM PostLangs ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PostLang>(query);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<PostLang>> GetAllBySpecificLang(int languageId)
        {
            var query = "SELECT * FROM PostLangs WHERE LanguageId = @LanguageId";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PostLang>(query, new { LanguageId = languageId });
                return result.ToList();
            }
        }

        public async Task<PostLang> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM PostLangs WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<PostLang>(query, new {Id = id});
                return result;
            }
        }

        public async Task<PostLang> GetBySpecificLang(int postId, int languageId)
        {
            var query = "SELECT * FROM PostLangs WHERE PostId = @PostId and LanguageId = @LanguageId ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<PostLang>(query, new { PostId = postId, LanguageId = languageId});
                return result;
            }
        }

        public async Task<IReadOnlyList<PostLang>> GetPostId(int postId)
        {
            var query = "SELECT * FROM PostLangs WHERE PostId = @PostId";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PostLang>(query, new {PostId = postId});
                return result.ToList();
            }
        }

        public async Task<PostLang> UpdateAsync(PostLang entity)
        {
            var sql = "UPDATE PostLangs SET Title = @Title, Body=@Body ,ShortDescription=@ShortDescription WHERE Id=@Id ";
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

        public async Task<PostLang> UpdateTransactional(PostLang entity)
        {
            var sql = "UPDATE PostLangs SET Title = @Title, Body=@Body ,ShortDescription=@ShortDescription WHERE Id=@Id ";
            var result = await _session.Connection.ExecuteAsync(sql, entity,_session.Transaction);
            if(result > 0)
            {
                return entity;
            }
            return null; 
        }
    }
}
