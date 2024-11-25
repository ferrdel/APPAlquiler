using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //Check if model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Check if user exists
            //var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == loginDto.Email);

            //Check if user is null, return unauthorized
            if (user == null) return Unauthorized("Invalid email or password");

            //Check if password is correct
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            //If password is not correct, return unauthorized
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);

            //Return user
            return Ok(new UserDto
            {
                Id = user.Id,
                //Email = user.Email,
                //ProfileId = user.ProfileId,//verificar si es de esta manera
                Token = await _tokenService.GenerateToken(user) //Generate token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            //Check if model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Check if user exists
            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null) return BadRequest("User already exists");

            //Create user
            var user = new User
            {
                UserName= registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DNI = registerDto.DNI,
                //Active = registerDto.Active,
                //ProfileId=registerDto.ProfileId

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //Check if user was created
            if (!result.Succeeded) return BadRequest(result.Errors);

            //Add role
            var roleResult = await _userManager.AddToRoleAsync(user, "ADMIN");
            //Check if role was added
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //Return user
            return Ok(new UserDto
            {
                Id = user.Id,
                //Email = user.Email,
                Token = await _tokenService.GenerateToken(user) //Generate token
            });
        }
    }
}
