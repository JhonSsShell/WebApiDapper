using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using webApi.Models;

/*
    Los repositorios en C#, funcionan principalmente para hacer las ejecuciones de los scripts de sql
    Donde es mucho mas facil

    Cuando se trae algun dato, se utiliza el return y se respectivo metodo para obtener esos datos
    Cuando se va a ejecutar algo, es decir agregar, actualizar o eliminar, de utiliza .ExecuteAsync
*/

namespace webApi.Repositories
{
    public class UserRepository
    {
        // Variable para conectar con la base de datos
        private readonly string _connectionString;

        // Metodo para conectar con la base de datos
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Metodo para conectar con la base de datos de manera asincrona
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM users;";
                return await db.QueryAsync<User>(sql);
            }
        }

        // Metodo para obtener un usuario por id
        public async Task<User> GetUserByIdAsync(int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM users WHERE Id = @Id;";
                return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            }
        }

        // Metodo para agregar un nuevo usuario a la base de datos
        public async Task AddUserAsync(UserDto user)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "INSERT INTO users(Name, Email) VALUES (@Name, @Email);";
                await db.ExecuteAsync(sql, user);
            }
        }

        // Metodo para actualizar un usuario por id en la base de datos
        public async Task ModifyUserAsync(int id, UserDto user)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "UPDATE users SET Name = @Name, Email = @Email WHERE Id = @Id";
                await db.ExecuteAsync(sql, new { user.Name, user.Email, Id = id });
            }
        }

        // Metodo para eliminar un usuario en la base de datos
        public async Task DeleteUserAsync (int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sql = "DELETE FROM users WHERE Id = @Id";
                await db.ExecuteAsync(sql, new { Id = id });
            }
        }

    }
}
