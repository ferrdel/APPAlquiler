using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly AlquilerDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, AlquilerDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //Check if model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Check if user exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            //var user = _userManager.Users.SingleOrDefault(u => u.UserName == loginDto.Email);         //Cambiamos el var user

            //Check if user is null, return unauthorized
            if (user == null) return Unauthorized("Invalid email or password");

            //Check if password is correct
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            //If password is not correct, return unauthorized
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            //var roles = await _userManager.GetRolesAsync(user);           //No aparecia en el video

            //Return user
            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
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
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DNI = registerDto.DNI,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                City = registerDto.City,
                Country = registerDto.Country,
                Gender = registerDto.Gender,
                Active = true                           //Al registrase se lo guarda como Activo true

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //Check if user was created
            if (!result.Succeeded) return BadRequest(result.Errors);

            //Add role
            var roleResult = await _userManager.AddToRoleAsync(user, "USER");
            //Check if role was added
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //Return user
            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user) //Generate token
            });

        }
        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Acceder al claim nameid
            var users = await _context.Set<User>().Where(o => o.Active == true).ToListAsync();        //Trae exclusivamente todos los usuarios activos

            if (users == null)
            {
                return BadRequest("Users not found");
            }


            return users;
        }
    }
}
