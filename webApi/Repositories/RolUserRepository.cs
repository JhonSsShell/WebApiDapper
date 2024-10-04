using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using webApi.Models;

namespace webApi.Repositories
{
    public class RolUserRepository
    {
        private readonly string _connectionString;

        public RolUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<RolUser>> GetAllRolUserAsync()
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT id, user_id AS UserId, rol_id AS RolId FROM rol_users;";
                var data = await db.QueryAsync<RolUser>(sql);

                Console.WriteLine(data);

                return data;
                //return await db.QueryAsync<RolUser>(sql);
            }
        }

        public async Task<RolUser> GetRolUserByIdAsync(int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM rol_users WHERE Id = @Id;";
                return await db.QueryFirstOrDefaultAsync<RolUser>(sql, new { Id = id });
            }
        }

        public async Task AddRolUserAsync(RolUserDto rolUserDto)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "INSERT INTO rol_users(user_id, rol_id) VALUES (@UserId, @RolId);";
                await db.ExecuteAsync(sql, rolUserDto);
            }
        }

        public async Task ModifyRolUserAsync (int id, RolUserDto rolUserDto)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "UPDATE rol_users SET user_id = @UserId, rol_id = @RolId WHERE Id = @Id;";
                await db.ExecuteAsync(sql, new { rolUserDto.UserId, rolUserDto.RolId, Id = id });
            }
        }

        public async Task DeleteRolUserAsync  (int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "DELETE FROM rol_users WHERE Id = @Id;";
                await db.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
