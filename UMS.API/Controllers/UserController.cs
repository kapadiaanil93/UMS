using UMS.Application.Interfaces;
using UMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService ;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, message = "User data is required." });
            }

            var entity = await _userService.Authenticate(new AuthenticationRequest { Email = dto.Email, Password = dto.Password });
            if (entity == null)
            {
                return Unauthorized(new { message = "Invalid user and password" });
            }
            return Ok(entity);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(User _user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid user data." });
            }
            return Ok(await _userService.Register(_user));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
