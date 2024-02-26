using Microsoft.AspNetCore.Mvc;
using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Interfaces.Service;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("GetUsers")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _serviceManager.userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUser")]
        [ProducesResponseType(200, Type=typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> GetUser(int userId)
        {
            var users = await _serviceManager.userService.GetUserById(userId);
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> CreateUser(UserRequestDto userDto)
        {
            var user = await _serviceManager.userService.CreateUser(userDto);
            return Ok(user);
        }

        [HttpPost("FlagUser")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> FlagUser(FlagUserRequestDto flagUserRequestDto)
        {
            var user = await _serviceManager.userService.UserFlagger(flagUserRequestDto.UserId, true);
            return Ok(user);
        }

        [HttpPost("UnFlagUser")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> UnFlagUser(FlagUserRequestDto flagUserRequestDto)
        {
            var user = await _serviceManager.userService.UserFlagger(flagUserRequestDto.UserId, false);
            return Ok(user);
        }

        [HttpPost("BackDateUserCreationDate")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> BackDateUserCreationDate(AddDateRequestDto addDateRequestDto)
        {
            var user = await _serviceManager.userService.BackDateUserCreationDate(addDateRequestDto.UserId, addDateRequestDto.DaysToBackDate);
            return Ok(user);
        }


        [HttpDelete("DeleteUser")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<UserResponseDto>))]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _serviceManager.userService.DeleteUser(userId);
            return Ok(user);
        }
    }
}
