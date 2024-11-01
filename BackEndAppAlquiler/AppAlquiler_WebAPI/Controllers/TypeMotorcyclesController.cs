using AppAlquiler_DataAccessLayer.Data;
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
        private readonly AlquilerDbContext _context;

        public TypeMotorcyclesController(AlquilerDbContext context)
        {
            _context = context;
        }

        //GET: api/TypeMotorcycle
        [HttpGet]
        public async Task<IActionResult> GetAllType() //obtener todas las marcas
        {
            var typeMot = _context.TypeMotorcycles.ToListAsync();
            return Ok(typeMot);
        }

        [HttpPost]
        public async Task<IActionResult> PostTypeMotorcycle([FromBody] TypeMotorcycleDto typeMotorcycleDto)
        {
            if (ModelState.IsValid)
            {
                var typeMot = new TypeMotorcycle
                {
                    Name = typeMotorcycleDto.Name,
                    State = typeMotorcycleDto.State
                };

                _context.TypeMotorcycles.Add(typeMot);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTypeMotorcycle", new { id = typeMot.Id }, typeMot);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //Metodo eliminar, cambiando a false el estado de la marca

        [HttpDelete("{id}", Name = "DeleteTypeMotorcycle")]
        public async Task DeleteType(int id)
        {
            var typeMot = await _context.TypeMotorcycles.FindAsync(id);

            if (typeMot != null && typeMot.State)
            {
                typeMot.State = false;
                await _context.SaveChangesAsync();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, [FromBody] TypeMotorcycle typeM)
        {
            if (id != typeM.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(typeM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        private bool TypeMExists(int id)
        {
            return _context.TypeMotorcycles.Any(m => m.Id == id);
        }
    }
}
