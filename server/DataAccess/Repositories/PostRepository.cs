using Dapper;
using Microsoft.Data.SqlClient;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;
using server.ViewModel.Commons;
using server.ViewModel.Posts;

namespace server.DataAccess.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model);
    }
    public class PostRepository :  IPostRepository
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
            var sql = "INSERT INTO Posts (CategoryId,Title, Thumbnail, Body ,ShortDescription , CreatedDate , Views , Author) VALUES (@CategoryId, @Title,@Thumbnail, @Body ,@ShortDescription, @CreatedDate, @Views, @Author) ; SELECT CAST(SCOPE_IDENTITY() as int) ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
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
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return result;
            }
        }

        public async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            var query = "SELECT * FROM Posts ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Post>(query);
                return result.ToList();
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Posts WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Post>(query, new { Id = id });
                return result;
            }
        }

        public  async Task<PaginatedList<PostModel>> GetPagedPost(PostSearchModel model)
        {
            List<PostModel> items;
            var offSet = model.Pagesize*(model.PageNumber -1);
            int totalCount;
            var query = "Select * From Posts LIMIT @PageSize  OFFSET @OFFSET ; SELECT COUNT(*) FROM Posts ";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                using (var multi = await connection.QueryMultipleAsync(query, new { @PageSize = model.Pagesize, @OFFSET = offSet}))
                {
                    items = multi.Read<PostModel>().ToList();
                    totalCount = multi.ReadFirst<int>();
                }
            }
            return new PaginatedList<PostModel>(items , totalCount ,model.PageNumber,model.Pagesize);
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            var sql = "UPDATE Posts SET CategoryId = @CategoryId , Title = @Title, Thumbnail = @Thumbnail, Body=@Body ,ShortDescription=@ShortDescription ,Views = @Views , Author=@Author WHERE Id=@Id ";
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
