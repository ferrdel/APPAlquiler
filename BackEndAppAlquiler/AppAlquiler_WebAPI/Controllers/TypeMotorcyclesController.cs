using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeMotorcyclesController : ControllerBase
    {
        private readonly ITypeMotorcycleService _typeMotorcycleService;

        public TypeMotorcyclesController(ITypeMotorcycleService typeMotorcycleService)
        {
            _typeMotorcycleService = typeMotorcycleService;
        }

        //GET: api/TypeMotorcycle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeMotorcycleDto>>> GetAllType() //obtener todas las marcas
        {
            var typeMot = await _typeMotorcycleService.GetAllTypeMotorcycleAsync();
            return Ok(typeMot);
        }

        [HttpGet("{id}", Name = "GetTypeMotorcycle")]
        public async Task<ActionResult<TypeMotorcycleDto>> GetTypeMotorcycle(int id)
        {
            var typeMotorcycle = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);

            if (typeMotorcycle == null)
            {
                return NotFound("Type of Motorcycle not found");
            }

            var typeMotorcycleDto = new TypeMotorcycleDto
            {
                Id = typeMotorcycle.Id,
                Name = Enum.GetName(typeMotorcycle.Name),
                Active = typeMotorcycle.Active,
            };

            return Ok(typeMotorcycle);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutType(int id, [FromBody] TypeMotorcycleDto typeMotorcycleDto)
        {
            if (id != typeMotorcycleDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var typeMotorcycle = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);
            if (typeMotorcycle == null)
                return NotFound("TypeMotorcycle not found");
            if (typeMotorcycle.Active != typeMotorcycleDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            typeMotorcycle.Name = Enum.Parse<NameTypeMotorcycle>(typeMotorcycleDto.Name);

            var succeeded = await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMotorcycle);
            if (!succeeded) return BadRequest("fallo");
            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostTypeMotorcycle([FromBody] TypeMotorcycleDto typeMotorcycleDto)
        {
            try
            {
                var typeMotorcycle = new TypeMotorcycle
                {
                    Name = Enum.Parse<NameTypeMotorcycle>(typeMotorcycleDto.Name),
                    Active = typeMotorcycleDto.Active
                };

                var succeeded = await _typeMotorcycleService.AddTypeMotorcycleAsync(typeMotorcycle);
                if (succeeded)
                    return CreatedAtAction("GetTypeMotorcycle", new { Id = typeMotorcycle.Id }, typeMotorcycle);
                else
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Metodo eliminar, cambiando a false el estado de la marca
        [HttpDelete("{id}", Name = "DeleteTypeMotorcycle")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var typeMot = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);

            if (typeMot != null && typeMot.Active)
            {
                typeMot.Active = false;
                await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMot);
            }
            else return BadRequest("Not found typeMotorcycle or not Active");

            return NoContent();
        }

        
        [HttpPatch("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ActivateModel(int id)
        {
            var typeMot = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);
            if (typeMot != null && !typeMot.Active)
            {
                typeMot.Active = true;
                await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMot);
            }
            else return BadRequest("Not found typeMotorcycle or Active");

            return NoContent();
        }

        private bool TypeMExists(int id)
        {
            var exists = _typeMotorcycleService.GetTypeMotorcycleAsync(id).Result;
            return exists != null;
        }
    }
}
