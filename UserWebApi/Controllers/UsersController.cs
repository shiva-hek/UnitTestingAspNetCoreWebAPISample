using Microsoft.AspNetCore.Mvc;
using UserApplication.ServiceInterfaces;
using UserDomain;


namespace UserWebApi.Controllers
{
    
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var users= await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("users/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost("users")]
        public async Task Create([FromBody] User user)
        {
            await _userService.Add(user);
        }
    }
}
