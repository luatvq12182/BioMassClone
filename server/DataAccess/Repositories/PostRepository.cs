using Dapper;
using MySqlConnector;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
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
        public Task<PaginatedList<PostViewModel>> SearchPost(int languageId, int? categoryId, bool? showOnHomePage, int pageNumber = 1, int pageSize = 10);
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

        public async Task<IReadOnlyList<Post>> SearchPost(bool? showOnHomePage, int? categoryId)
        {
            var whereStatement = " WHERE ";
            if(categoryId.HasValue && categoryId > 0)
            {
                whereStatement += " CategoryId = @CategoryId AND ";
            }
            if (showOnHomePage.HasValue && showOnHomePage.Value)
            {
                whereStatement += " ShowOnHomePage = 1 AND ";
            }
            whereStatement = whereStatement.Substring(0, whereStatement.Length - 4);

            var query = "SELECT * FROM Posts " + whereStatement;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Post>(query , new {CategoryId = categoryId, ShowOnHomePage = showOnHomePage});
                return result.ToList();
            }
        }
        public async Task<PaginatedList<PostViewModel>> SearchPost(int languageId , int? categoryId , bool? showOnHomePage, int pageNumber = 1 , int pageSize = 10)
        {
            var whereStatement = " WHERE postLang.LanguageId = @LanguageId AND ";
            if (categoryId.HasValue && categoryId > 0)
            {
                whereStatement += " post.CategoryId = @CategoryId AND ";
            }
            if (showOnHomePage.HasValue && showOnHomePage.Value)
            {
                whereStatement += " post.ShowOnHomePage = 1 AND ";
            }
            whereStatement = whereStatement.Substring(0, whereStatement.Length - 4);

            List<PostViewModel> items;

            var offSet = pageSize * (pageNumber - 1);
            int totalCount;

            var query = @"SELECT postLang.Id AS Id , 
                        post.Id AS PostId , 
                        post.CategoryId AS CategoryId , 
                        post.Thumbnail AS Thumbnail , 
                        post.CreatedDate AS CreatedDate , 
                        post.Views AS Views ,
                        post.Author AS Author , 
                        post.ShowOnHomePage AS ShowOnHomePage , 
                        postLang.LanguageId as LanguageId , 
                        postLang.Title AS Title , postLang.Body AS Body , 
                        postLang.ShortDescription AS ShortDescription , 
                        postLang.Slug AS Slug 
                        FROM Posts post LEFT JOIN PostLangs postLang On post.Id = postLang.PostId " + whereStatement + " ORDER BY post.CreatedDate " + 
                        @" LIMIT @PageSize OFFSET @OffSet ;
                        SELECT COUNT(*) FROM Posts post LEFT JOIN PostLangs postLang On post.Id = postLang.PostId " + whereStatement ;

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MySqlConn")))
            {
                connection.Open();
                using (var multi = await connection.QueryMultipleAsync(query, new {LanguageId = languageId , PageSize = pageSize , OffSet = offSet, CategoryId = categoryId }))
                {
                    items = multi.Read<PostViewModel>().ToList();
                    totalCount = multi.ReadFirst<int>();
                }
            }
            return new PaginatedList<PostViewModel>(items, totalCount, pageNumber, pageSize);
        }
    }
}
