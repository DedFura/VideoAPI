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
    public class CategoryRepository : ICategoryRepository {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task<int> Add(CategoryModel entity) {
            var sql = "INSERT INTO Category (Name) Values (@Name);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();

                var affectedRow = await connection.ExecuteAsync(sql, entity);

                return affectedRow;
            }
        }

        public async Task<int> Delete(int id) {
            var sql = "DELETE FROM Category WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRow;
            }
        }

        public async Task<CategoryModel> Get(int id) {
            var sql = "SELECT * FROM Category WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<CategoryModel>(sql, new { Id = id });
                return affectedRow.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<CategoryModel>> GetAll() {
            var sql = "SELECT * FROM Category;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<CategoryModel>(sql);
                return affectedRow;
            }
        }
        public async Task<int> Update(CategoryModel entity) {
            var sql = "UPDATE Category SET Name = @Name WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, entity);
                return affectedRow;
            }
        }
    }
}
