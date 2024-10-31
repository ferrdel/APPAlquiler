using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using System.Runtime.ConstrainedExecution;
using AppAlquiler_WebAPI.Infrastructure.Dto;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public MotorcyclesController(AlquilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Motorcycles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorcycle>>> GetMotorcycles()
        {
            return await _context.Motorcycles.ToListAsync();
        }

        // GET: api/Motorcycles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorcycle>> GetMotorcycle(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);

            if (motorcycle == null)
            {
                return NotFound();
            }

            return motorcycle;
        }

        // PUT: api/Motorcycles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorcycle(int id, Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return BadRequest();
            }

            _context.Entry(motorcycle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Motorcycles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostMotorcycle([FromBody]MotorcycleDto motorcycleDto)
        {
            if (ModelState.IsValid)
            {
                var motorcycle = new Motorcycle
                {
                    Description = motorcycleDto.Description,
                    GasolineConsumption = motorcycleDto.GasolineConsumption,
                    LuggageCapacity = motorcycleDto.LuggageCapacity,
                    PassengerCapacity = motorcycleDto.PassengerCapacity,
                    Fuel = motorcycleDto.Fuel,
                    State = motorcycleDto.State,
                    Price = motorcycleDto.Price,
                    ModelID = motorcycleDto.ModelId,
                    BrandId = motorcycleDto.BrandId,
                    
                    Abs=motorcycleDto.Abs,
                    cilindrada=motorcycleDto.cilindrada,
                    TypeId=motorcycleDto.TypeId
                };

                _context.Motorcycles.Add(motorcycle);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetMotorcycle", new { Id = motorcycle.Id }, motorcycle);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Motorcycles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycle(int id)
        {
            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotorcycleExists(int id)
        {
            return _context.Motorcycles.Any(e => e.Id == id);
        }
    }
}
