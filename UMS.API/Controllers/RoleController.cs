using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Interfaces;
using UMS.Domain.Entities;
using UMS.Domains.Entities;

namespace UMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleSaveEntity _role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid role data." });
            }
            return Ok(await _service.Create(_role));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {                          
            var role = await _service.Delete(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateEntity dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid role data." });
            }
            return Ok(await _service.Update(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roleData = await _service.GetAll();
            if (roleData == null)
            {
                return NotFound();
            }
            return Ok(roleData);
        }
    }
}
