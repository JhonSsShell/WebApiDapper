using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories;

namespace webApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolRepository _rolRepository;

        public RolesController(RolRepository rolRepository) 
        {
            _rolRepository = rolRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetAllRoles()
        {
            var roles = await _rolRepository.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRolById(int id)
        {
            var rol = await _rolRepository.GetRolByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult> AddRol([FromBody] RolDto rolDto)
        {
            await _rolRepository.AddRolAsync(rolDto);
            return CreatedAtAction(nameof(GetAllRoles), new { name = rolDto.Name });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyRol(int id,  [FromBody] RolDto rolDto)
        {
            await _rolRepository.ModifyRolAsync(id, rolDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRol(int id)
        {
            await _rolRepository.DeleteRolAsync(id);
            return NoContent();
        }
    }
}
