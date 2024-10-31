using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public BikesController(AlquilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBikes()
        {
            return await _context.Bikes.ToListAsync();
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);

            if (bike == null)
            {
                return NotFound();
            }

            return bike;
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Bike bike)
        {
            if (id != bike.Id)
            {
                return BadRequest();
            }

            _context.Entry(bike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeExists(id))
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

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBike([FromBody] BikeDto bikeDto)
        {
            if (ModelState.IsValid)
            {
                var bike = new Bike
                {
                    Description = bikeDto.Description,
                    GasolineConsumption = bikeDto.GasolineConsumption,
                    LuggageCapacity = bikeDto.LuggageCapacity,
                    PassengerCapacity = bikeDto.PassengerCapacity,
                    Fuel = bikeDto.Fuel,
                    State = bikeDto.State,
                    Price = bikeDto.Price,
                    ModelID = bikeDto.ModelId,
                    BrandId = bikeDto.BrandId,
                    Whell = bikeDto.Whell,
                    FrameSize = bikeDto.FrameSize,
                    NumberSpeeds = bikeDto.NumberSpeeds
                };

                _context.Bikes.Add(bike);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetBike", new { Id = bike.Id }, bike);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeExists(int id)
        {
            return _context.Bikes.Any(e => e.Id == id);
        }
    }
}
