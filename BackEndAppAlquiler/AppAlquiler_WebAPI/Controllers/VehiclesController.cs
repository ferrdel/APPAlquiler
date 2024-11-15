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
    public class VehiclesController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public VehiclesController(AlquilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles= _context.Boats.ToListAsync();
            return Ok(vehicles);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleID(int id)
        {
            var vehicle = await _context.Boats.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound("Vehicle not found");
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostVehicle([FromBody] VehicleDto vehicleDto)
        {
            if (ModelState.IsValid)
            {
                var vehicle = new Boat
                {
                    Description = vehicleDto.Description,
                    GasolineConsumption = vehicleDto.GasolineConsumption,
                    LuggageCapacity = vehicleDto.LuggageCapacity,
                    PassengerCapacity = vehicleDto.PassengerCapacity,
                    Fuel = vehicleDto.Fuel,
                    State = Enum.Parse<State>(vehicleDto.State),
                    Active = vehicleDto.Active,
                    Price = vehicleDto.Price,
                    Image = vehicleDto.Image,
                    ModelId = vehicleDto.ModelId,
                    BrandId = vehicleDto.BrandId
                };

                _context.Boats.Add(vehicle);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetModelo", new { Id = vehicle.Id }, vehicle);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Boats.FindAsync(id);

            if (vehicle != null && vehicle.Active)
            {
                vehicle.Active = false;
                await _context.SaveChangesAsync();
            }


            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.Boats.Any(e => e.Id == id);
        }
    }
}
