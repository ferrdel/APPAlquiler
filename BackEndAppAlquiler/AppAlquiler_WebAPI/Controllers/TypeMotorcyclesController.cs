using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
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
                Name = typeMotorcycle.Name,
                Active = typeMotorcycle.Active,
            };

            return Ok(typeMotorcycle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, [FromBody] TypeMotorcycleDto typeMotorcycleDto)
        {
            if (id != typeMotorcycleDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var typeMotorcycle = new TypeMotorcycle
            {
                Id = (int)typeMotorcycleDto.Id,
                Name = typeMotorcycleDto.Name,
                Active = typeMotorcycleDto.Active,
            };

            try
            {
                var succeeded = await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMotorcycle);
                if (succeeded)
                    return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TypeMExists(id))
                {
                    return NotFound("TypeMotorcycle not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostTypeMotorcycle([FromBody] TypeMotorcycleDto typeMotorcycleDto)
        {
            if (ModelState.IsValid)
            {
                var typeMot = new TypeMotorcycle
                {
                    Name = typeMotorcycleDto.Name,
                    Active = typeMotorcycleDto.Active
                };

                var succeeded = await _typeMotorcycleService.AddTypeMotorcycleAsync(typeMot);
                if (succeeded)
                    return CreatedAtAction("GetTypeMotororcycle", new { Id = typeMot.Id }, typeMot);
                else
                    return BadRequest(ModelState);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //Metodo eliminar, cambiando a false el estado de la marca
        [HttpDelete("{id}", Name = "DeleteTypeMotorcycle")]
        public async Task DeleteType(int id)
        {
            var typeMot = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);

            if (typeMot != null && typeMot.Active)
            {
                typeMot.Active = false;
                await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMot);
            }
        }

        
        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateModel(int id)
        {
            var typeMot = await _typeMotorcycleService.GetTypeMotorcycleAsync(id);
            if (typeMot != null && !typeMot.Active)
            {
                typeMot.Active = true;
                await _typeMotorcycleService.UpdateTypeMotorcycleAsync(typeMot);
            }

            return NoContent();
        }

        //ToDo   ---- Verificar que este con el servicio
        private bool TypeMExists(int id)
        {
            return _typeMotorcycleService.GetTypeMotorcycleAsync(id) != null;
        }
    }
}
