using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories;

namespace webApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        // Variable para hacer llamado al repositorio, es decir donde estaran las consultas SQL
        private readonly UserRepository _userRepository;

        // Constructor para inicializar el repositorio
        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Metodo Get para obtener todos los usuarios de la base de datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // Metodo Get/{id} para obtener un usuario por id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Metodo Post para agregar un nuevo usuario a la base de datos
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            await _userRepository.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetAllUsers), new { name = userDto.Name }, userDto);
        }

        // Metodo Put/{id} para modificar un usuario por id
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyUser(int id, [FromBody] UserDto userDto)
        {
            await _userRepository.ModifyUserAsync(id, userDto);
            return NoContent();
        }

        // Metodo Delete/{id} para eliminar un usuario por id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

    }
}
