﻿using System;
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
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<CarDetailsDto>>> GetCars()
        {
            var succeeded = await _carService.GetAllCarAsync();
            var carDetails = succeeded.Select(car => new CarDetailsDto
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
                Image = car.Image,
                Model = car.Model.Name,

                //agregado brand
                Brand = car.Model.Brand.Name,
                //CAracteristicas Auto
                NumberDoors = car.NumberDoors,
                AirConditioning = car.AirConditioning,
                Transmission = car.Transmission,
                Airbag = car.Airbag,
                Abs = car.Abs,
                Sound = car.Sound,
                EngineLiters = car.EngineLiters
            });            
            return Ok(carDetails);
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
                Image = car.Image,
                ModelId = car.ModelId,
                BrandId = car.Model.BrandId,
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
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutCar(int id,[FromBody] CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            if (!CarExists(id))
            {
                return NotFound("Car not found");
            }
            //Verificacion de que existe el modelo
            if (!ModelExists(carDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest("Model not found");
            }

            var car = await _carService.GetCarAsync(id);
            if (car == null)
                return NotFound("Car not found");
            
            if (car.Active != carDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            car.Description = carDto.Description;
            car.GasolineConsumption = carDto.GasolineConsumption;
            car.LuggageCapacity = carDto.LuggageCapacity;
            car.PassengerCapacity = carDto.PassengerCapacity;
            car.Fuel = carDto.Fuel;
            car.State = Enum.Parse<State>(carDto.State);
            car.Price = carDto.Price;
            car.Image = carDto.Image;
            car.ModelId = carDto.ModelId;
            //Caracteristicas Auto
            car.NumberDoors = carDto.NumberDoors;
            car.AirConditioning = carDto.AirConditioning;
            car.Transmission = carDto.Transmission;
            car.Airbag = carDto.Airbag;
            car.Abs = carDto.Abs;
            car.Sound = carDto.Sound;
            car.EngineLiters = carDto.EngineLiters;

            var succeeded = await _carService.UpdateCarAsync(car);
            if (!succeeded) return BadRequest("fallo");            
            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostCar([FromBody] CarDto carDto)
        {
            //Verificacion de que existe el modelo
            if (!ModelExists(carDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            try
            { 
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
                    Image = carDto.Image,
                    ModelId = carDto.ModelId,
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
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car != null && car.Active)
            {
                car.Active = false;
                await _carService.UpdateCarAsync(car);
            }
            else return BadRequest("Not found car or not Active");
            
            return NoContent();
        }

        [HttpPatch("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ActivateCar(int id)
        {
            var car = await _carService.GetCarAsync(id);
            if (car != null && !car.Active)
            {
                car.Active = true;
                await _carService.UpdateCarAsync(car);
            }
            else return BadRequest("Not found car or Active");
            
            return NoContent();
        }

        private bool CarExists(int id)
        {
            var exists =_carService.GetCarAsync(id).Result;
            return exists != null;
        }

        private bool ModelExists(int id)
        {
            var exists =_carService.GetModelByIdAsync(id).Result;
            return exists != null;
        }
    }
}
