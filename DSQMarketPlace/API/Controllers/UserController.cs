using Infrastructure.DTOs;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(ILogger<UserController> logger, IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("AddUser")]
        public async Task AddUser(UserToAddDTO user)
        {
            await _userService.AddUser(user);
        }
        [HttpGet("GetAllUsers")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }
        [HttpGet("GetCurrentVouchers")]
        public async Task<List<UserVoucherToReturnDTO>> GetCurrentVouchers(int userId)
        {
            return await _userService.GetCurrentVouchers(userId);
        }
        [HttpPost("Login")]
        public async Task<UserToReturnDTO> Login(UserToAddDTO loginUser)
        {
            User? user = await _userService.GetUserByNumber(loginUser.PhoneNumber);
            if (user != null)
            {
                return new UserToReturnDTO()
                {
                    Id = user.Id,
                    Token = _authService.CreateToken(user),
                    PhoneNumber = user.PhoneNumber
                };

            }
            else
            {
                throw new Exception("User Not Found");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserToReturnDTO>> getCurrentUser()
        {
            var context = HttpContext.User;
            Console.WriteLine(Request.Headers.Authorization);
            var id = this.User.FindFirst("userId")?.Value;
            var user = await _userService.GetUserById(Int32.Parse(id));

            return new UserToReturnDTO()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Token = _authService.CreateToken(user)
            };


        }
    }
}