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
    class EmployeeRepository : IEmployeeRepository {

        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task<int> Add(EmployeeModel entity) {
            var sql = "INSERT INTO Employee (Login, Password, Role) Values (@Login, @Password, @Role);";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();

                var affectedRow = await connection.ExecuteAsync(sql, entity);

                return affectedRow;
            }
        }

        public async Task<int> Delete(int id) {
            var sql = "DELETE FROM Employee WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRow;
            }
        }

        public async Task<EmployeeModel> Get(int id) {
            var sql = "SELECT * FROM Employee WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<EmployeeModel>(sql, new { Id = id });
                return affectedRow.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<EmployeeModel>> GetAll() {
            var sql = "SELECT * FROM Employee;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.QueryAsync<EmployeeModel>(sql);
                return affectedRow;
            }
        }
        public async Task<int> Update(EmployeeModel entity) {
            var sql = "UPDATE Employee SET Login = @Login, Password = @Password, Role = @Role WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                connection.Open();
                var affectedRow = await connection.ExecuteAsync(sql, entity);
                return affectedRow;
            }
        }
    }
}
