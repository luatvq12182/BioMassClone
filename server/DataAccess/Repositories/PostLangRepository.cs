﻿using Dapper;
using MySqlConnector;
using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Persistence;

namespace server.DataAccess.Repositories
{
    public interface IPostLangRepository : IGenericRepository<PostLang>
    {
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
            var sql = "INSERT INTO PostLangs (PostId, LangId , Title, Body ,ShortDescription) VALUES (@PostId,@LangId , @Title, @Body ,@ShortDescription) ; SELECT LAST_INSERT_ID() ";
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

        public async Task<PostLang> UpdateAsync(PostLang entity)
        {
            var sql = "UPDATE PostLangs SET PostId = @PostId, LangId = @LangId , Title = @Title, Body=@Body ,ShortDescription=@ShortDescription WHERE Id=@Id ";
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
