using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_WebAPI.Infrastructure.Dto;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userDto = new UserDto
            {
                //Email = user.Email,
                //Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DNI = user.DNI,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                Region = user.Region,
                Generate = user.Generate,
                Active = user.Active,

                //ProfileId = user.ProfileId
            };

            return userDto;

            return Ok(user);
        }

        
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,[FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("Id user mismatch");
            }

            var user = new User
            {
               // Email = userDto.Email,
                //Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DNI = userDto.DNI,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                City = userDto.City,
                Region = userDto.Region,
                Generate = userDto.Generate,
                Active = userDto.Active,

                //ProfileId = userDto.ProfileId
            };

            try
            {
                var succeeded = await _userService.UpdateUserAsync(user);
                if (succeeded) return Content("user ok!");
                else return BadRequest("Error");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    return NotFound("User not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserDto userDto)
        {            
            try
            {
                var user = new User
                {
                   // Email = userDto.Email,
                    //Password = userDto.Password,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    DNI = userDto.DNI,
                    PhoneNumber = userDto.PhoneNumber,
                    Address = userDto.Address,
                    City = userDto.City,
                    Region = userDto.Region,
                    Generate = userDto.Generate,
                    Active = userDto.Active,

                    //ProfileId = userDto.ProfileId
                };

                var succeeded = await _userService.AddUserAsync(user);
                if (succeeded)
                    return CreatedAtAction("GetUser", new { Id = user.Id }, user);
                else
                    return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return BadRequest("ModelState");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user != null && user.Active)
            {
                user.Active = false; // cambia el state a true para indicar que esta eliminado
                await _userService.UpdateUserAsync(user);
            }

            return NoContent();
        }

        //Se vuelve activar un usuario si fue dado de baja
        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user != null && !user.Active)
            {
                user.Active = true;
                await _userService.UpdateUserAsync(user);
            }

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userService.GetUserAsync(id) != null;
        }
    }
}
