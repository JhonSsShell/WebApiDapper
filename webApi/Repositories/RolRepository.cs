using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using webApi.Models;

namespace webApi.Repositories
{
    public class RolRepository
    {
        private readonly string _connectionString;

        public RolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Rol>> GetAllRolesAsync()
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM roles;";
                return await db.QueryAsync<Rol>(sql);
            }
        }

        public async Task<Rol> GetRolByIdAsync(int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM roles WHERE Id = @Id;";
                return await db.QueryFirstOrDefaultAsync<Rol>(sql, new { Id = id });
            }
        }

        public async Task AddRolAsync(RolDto rolDto)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "INSERT INTO roles(Name) VALUES (@Name);";
                await db.ExecuteAsync(sql, rolDto);
            }
        }

        public async Task ModifyRolAsync(int id, RolDto rolDto)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "UPDATE roles SET Name = @Name WHERE Id = @Id;";
                await db.ExecuteAsync(sql, new { rolDto.Name, Id = id });
            }
        }

        public async Task DeleteRolAsync (int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "DELETE FROM roles WHERE Id = @Id;";
                await db.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
