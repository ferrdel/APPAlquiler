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
    public class CarsController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public CarsController(AlquilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CarExists(id))
                {
                    return NotFound("Car not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Description = carDto.Description,
                    GasolineConsumption = carDto.GasolineConsumption,
                    LuggageCapacity = carDto.LuggageCapacity,
                    PassengerCapacity = carDto.PassengerCapacity,
                    Fuel = carDto.Fuel,
                    State = carDto.State,
                    Active = carDto.Active,
                    Price = carDto.Price,
                    ModelID = carDto.ModelId,
                    BrandId = carDto.BrandId,
                    //CAracteristicas Auto
                    NumberDoors = carDto.NumberDoors,
                    AirConditioning = carDto.AirConditioning,
                    Transmission=carDto.Transmission,
                    Airbag= carDto.Airbag,
                    Abs=carDto.Abs,
                    Sound=carDto.Sound,
                    EngineLiters=carDto.EngineLiters
                };

                _context.Cars.Add(car);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetCar", new { Id = car.Id }, car);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            car.Active = false;
            _context.Cars.Update(car);
                                                        //_context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
