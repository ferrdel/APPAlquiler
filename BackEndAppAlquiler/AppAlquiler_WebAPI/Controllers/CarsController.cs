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
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var succeded = await _carService.GetAllCarAsync();
            return Ok(succeded);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _carService.GetCarAsync(id);

            if (car == null)
            {
                return NotFound("Car Not found");
            }

            //Agregado devolver carDTO
            var carDTO = new CarDto
            {
                Id = car.Id,    //agregado para el front
                Description = car.Description,
                GasolineConsumption = car.GasolineConsumption,
                LuggageCapacity = car.LuggageCapacity,
                PassengerCapacity = car.PassengerCapacity,
                Fuel = car.Fuel,

                //parseo
                State = Enum.GetName(car.State),
                Active = car.Active,
                Price = car.Price,
                ModelId = car.ModelId,
                BrandId = car.BrandId,
                //CAracteristicas Auto
                NumberDoors = car.NumberDoors,
                AirConditioning = car.AirConditioning,
                Transmission = car.Transmission,
                Airbag = car.Airbag,
                Abs = car.Abs,
                Sound = car.Sound,
                EngineLiters = car.EngineLiters
            };

            return carDTO;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var car = new Car
            {
                Id = (int)carDto.Id, //agregado porque no llegaba id. Ademas castea porque podia es nullable
                Description = carDto.Description,
                GasolineConsumption = carDto.GasolineConsumption,
                LuggageCapacity = carDto.LuggageCapacity,
                PassengerCapacity = carDto.PassengerCapacity,
                Fuel = carDto.Fuel,
                //State = stateEnum,
                State = Enum.Parse<State>(carDto.State),
                Active = carDto.Active,
                Price = carDto.Price,
                ModelId = carDto.ModelId,
                BrandId = carDto.BrandId,
                //CAracteristicas Auto
                NumberDoors = carDto.NumberDoors,
                AirConditioning = carDto.AirConditioning,
                Transmission = carDto.Transmission,
                Airbag = carDto.Airbag,
                Abs = carDto.Abs,
                Sound = carDto.Sound,
                EngineLiters = carDto.EngineLiters
            };

            try
            {
                var succeeded = await _carService.UpdateCarAsync(car);
                if (succeeded) NoContent();
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
                //Verificacion de que existe la marca
                if (!BrandExists(carDto.BrandId))
                {
                    ModelState.AddModelError("BrandId", "Brand Id Not found.");
                    return BadRequest("brand");
                }

                //Verificacion de que existe el modelo
                if (!ModelExists(carDto.ModelId))
                {
                    ModelState.AddModelError("ModelId", "Model Id Not found.");
                    return BadRequest("hla");
                }

                var car = new Car
                {
                    Description = carDto.Description,
                    GasolineConsumption = carDto.GasolineConsumption,
                    LuggageCapacity = carDto.LuggageCapacity,
                    PassengerCapacity = carDto.PassengerCapacity,
                    Fuel = carDto.Fuel,
                    State = Enum.Parse<State>(carDto.State),
                    Active = carDto.Active,
                    Price = carDto.Price,
                    ModelId = carDto.ModelId,
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

                var succeeded = await _carService.AddCarAsync(car);
                if (succeeded)
                    return CreatedAtAction("GetCar", new { Id = car.Id }, car);
                else
                    return BadRequest(ModelState);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car != null && car.Active)
            {
                car.Active = false;
                await _carService.UpdateCarAsync(car);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateCar(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car != null && !car.Active)
            {
                car.Active = true;
                await _carService.UpdateCarAsync(car);
            }

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _carService.GetCarAsync(id) != null;
        }

        private bool BrandExists(int id)
        {
            var estado = _carService.GetBrandByIdAsync(id) != null? true:false;
            return estado;
        }
        private bool ModelExists(int id)
        {
            return _carService.GetModelByIdAsync(id) != null;
        }
    }
}
