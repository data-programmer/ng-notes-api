using System.Threading.Tasks;
using NGNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NGNotesAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace NGNotesAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("SignIn/{Username}")]
        public async Task<ActionResult<UserModel>> SignIn(string Username)
        {
            var User = await _userService.SignIn(Username);
            if (User == null) return NotFound(User);

            return Ok(User);
        }

        [HttpGet("GetUser/{UserId}")]
        public async Task<ActionResult<UserModel>> GetUser(int UserId)
        {
            var User = await _userService.GetUser(UserId);
            if (User == null) return NotFound();

            return Ok(User);
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] UserModel User)
        {
            var NewUser = await _userService.RegisterUser(User);
            if (NewUser == null) return NotFound();

            return Ok(NewUser);
        }

        [HttpPut("UpdateUser")]
        public ActionResult<UserModel> UpdateUser([FromBody] UserModel User)
        {
            var UpdatedUser = _userService.UpdateUser(User);
            if (UpdatedUser == null) return NotFound();

            return Ok(UpdatedUser);
        }

        [HttpDelete("DeleteUser/{UserId}")]
        public ActionResult<UserModel> DeleteUser(int UserId)
        {
            var DeleteUser = _userService.DeleteUser(UserId);
            if (DeleteUser == null) return NotFound();

            return Ok(DeleteUser);
        }
    }
}