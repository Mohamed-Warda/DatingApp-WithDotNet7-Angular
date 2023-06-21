using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context ,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        

       

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
        {

            if(await UserExists(registerDto.userName))
            {
                return BadRequest("User Name Is Taken");
            }
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.userName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key

            };

             _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto { Username = user.UserName, Tokens = _tokenService.CreateToken(user) };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.userName);
            if (user == null) 
                return Unauthorized("Invalid User Name");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputedHashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < ComputedHashedPassword.Length;i++ )
            {
                if (ComputedHashedPassword[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }

            return new UserDto { Username = user.UserName, Tokens = _tokenService.CreateToken(user) };

        }





        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
