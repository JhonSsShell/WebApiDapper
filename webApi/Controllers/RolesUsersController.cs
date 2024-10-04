using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories;

namespace webApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesUsersController : ControllerBase
    {
        private readonly RolUserRepository _rolUserRepository;

        public RolesUsersController(RolUserRepository rolUserRepository) 
        {
            _rolUserRepository = rolUserRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolUser>>> GetAllRolesUsers()
        {
            var rolesUsers = await _rolUserRepository.GetAllRolUserAsync();
            return Ok(rolesUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolUser>> GetRolUserById(int id)
        {
            var rolUser = await _rolUserRepository.GetRolUserByIdAsync(id);
            if (rolUser == null)
            {
                return NotFound();
            }
            return Ok(rolUser);
        }

        [HttpPost]
        public async Task<ActionResult> AddRolUser([FromBody] RolUserDto rolUserDto)
        {
            await _rolUserRepository.AddRolUserAsync(rolUserDto);
            return CreatedAtAction(nameof(GetAllRolesUsers), new { userId = rolUserDto.UserId });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyRolUser(int id, [FromBody] RolUserDto rolUserDto)
        {
            await _rolUserRepository.ModifyRolUserAsync(id, rolUserDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRolUser(int id)
        {
            await _rolUserRepository.DeleteRolUserAsync(id);
            return NoContent();
        }
    }
}
