using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VideoAPI.Models.Models;
using VideoAPI.Services.Interfaces;

namespace TaskManagementApp.Infrastructure.Repository {
    public class TaskRepository : ITaskRepository {

        private readonly IConfiguration _configuration;

        public TaskRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task<int> Add(VideoModel entity) {
            var sql = "INSERT INTO Video (Name, Path, CategoryId, IsCorrect) Values (@Name, @Path, @CategoryId, @IsCorrect);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();

                var affectedRow = await connection.ExecuteAsync(sql, entity);

                return affectedRow;
            }
        }
        public async Task<int> Add(VideoVM entity) {

            var sql = "INSERT INTO Video (Name, Path, CategoryId, IsCorrect) Values (@Name, @Path, @CategoryId, @IsCorrect);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, entity.VideoModel);

                return affectedRow;
            }
        }

        public async Task<int> Add(List <VideoModel> entity) {
            var sql = "INSERT INTO Video (Name, Path, CategoryId, IsCorrect) Values (@Name, @Path, @CategoryId, @IsCorrect);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();

                var affectedRow = await connection.ExecuteAsync(sql, entity);

                return affectedRow;
            }
        }

        public async Task<int> Delete(int id) {
            var sql = "DELETE FROM Video WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRow;
            }
        }

        public async Task<VideoModel> Get(int id) {
            var sql = "SELECT * FROM VIDEO WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<VideoModel>(sql, new { Id = id });
                return affectedRow.FirstOrDefault(); // возвращаем первый из списка
            }
        }

        public async Task<IEnumerable<VideoModel>> GetAll() {
            var sql = "SELECT * FROM VIDEO;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<VideoModel>(sql);
                return affectedRow;
            }
        }

        public async Task<int> Update(VideoModel entity) {
            var sql = "UPDATE Video SET Name = @Name, Path = @Path, CategoryId = @CategoryId, IsCorrect = @IsCorrect WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, entity);
                return affectedRow;
            }
        }
    }
}
