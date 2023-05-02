using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppi.Data.Repositories;
using WebAppi.Model;

namespace WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetUserDetails(int id)
        {
            return Ok(await _userRepository.GetDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _userRepository.InsertUser(user);
            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userRepository.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(new Model.User { Id = id });
            return NoContent();
        }
    }
}   
