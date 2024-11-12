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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using System.Runtime.ConstrainedExecution;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public BikesController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDto>>> GetBikes()
        {
            var bikes = await _bikeService.GetAllBikeAsync();
            return Ok(bikes);
        }

        // GET: api/Bikes/5
        [HttpGet("{id}", Name = "GetBike")]
        public async Task<ActionResult<BikeDto>> GetBike(int id)
        {
            var bike = await _bikeService.GetBikeAsync(id);

            if (bike == null)
            {
                return NotFound("Bike not found");
            }

            var bikeDTO = new BikeDto
            {
                Id = bike.Id,    //agregado para el front
                Description = bike.Description,
                GasolineConsumption = bike.GasolineConsumption,
                LuggageCapacity = bike.LuggageCapacity,
                PassengerCapacity = bike.PassengerCapacity,
                Fuel = bike.Fuel,

                //parseo
                State = Enum.GetName(bike.State),
                Active = bike.Active,
                Price = bike.Price,
                ModelId = bike.ModelId,
                BrandId = bike.BrandId,
                //Caracteristicas Bike
                Whell = bike.Whell,
                FrameSize = bike.FrameSize,
                NumberSpeeds = bike.NumberSpeeds
            };

            return bikeDTO;

            return Ok(bike);
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id,[FromBody] BikeDto bikeDto)
        {
            if (id != bikeDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var bike = new Bike
            {
                Id = (int)bikeDto.Id, //agregado porque no llegaba id. Ademas castea porque podia es nullable
                Description = bikeDto.Description,
                GasolineConsumption = bikeDto.GasolineConsumption,
                LuggageCapacity = bikeDto.LuggageCapacity,
                PassengerCapacity = bikeDto.PassengerCapacity,
                Fuel = bikeDto.Fuel,
                //State = stateEnum,
                State = Enum.Parse<State>(bikeDto.State),
                Active = bikeDto.Active,
                Price = bikeDto.Price,
                ModelId = bikeDto.ModelId,
                BrandId = bikeDto.BrandId,
                //CAracteristicas Auto
                Whell = bikeDto.Whell,
                FrameSize = bikeDto.FrameSize,
                NumberSpeeds = bikeDto.NumberSpeeds
            };

            try
            {
                var succeeded = await _bikeService.UpdateBikeAsync(bike);
                if (succeeded) return Content("bien");
                else return BadRequest("Error");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BikeExists(id))
                {
                    return NotFound("Bike not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBike([FromBody] BikeDto bikeDto)
        {
            //Verificacion de que existe la marca
            if (!BrandExists(bikeDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand Id Not found.");
                return BadRequest(ModelState);
            }

            //Verificacion de que existe el modelo
            if (!ModelExists(bikeDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            //if (ModelState.IsValid)
            try
            {
                var bike = new Bike
                {
                    Description = bikeDto.Description,
                    GasolineConsumption = bikeDto.GasolineConsumption,
                    LuggageCapacity = bikeDto.LuggageCapacity,
                    PassengerCapacity = bikeDto.PassengerCapacity,
                    Fuel = bikeDto.Fuel,
                    State = Enum.Parse<State>(bikeDto.State),
                    Active = bikeDto.Active,
                    Price = bikeDto.Price,
                    ModelId = bikeDto.ModelId,
                    BrandId = bikeDto.BrandId,
                    Whell = bikeDto.Whell,
                    FrameSize = bikeDto.FrameSize,
                    NumberSpeeds = bikeDto.NumberSpeeds
                };

                var succeeded = await _bikeService.AddBikeAsync(bike);
                if (succeeded)
                    return CreatedAtAction("GetBike", new { Id = bike.Id }, bike);
                else
                    return BadRequest(ModelState);
            }
            catch (Exception )
            {
                return BadRequest("ModelState");
            }
            //return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            var bike = await _bikeService.GetBikeAsync(id);
            if (bike != null && bike.Active)
            {
                bike.Active = false; //cambia el estado a true para que quede como eliminado
                await _bikeService.UpdateBikeAsync(bike);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateBike(int id)
        {
            var bike = await _bikeService.GetBikeAsync(id);
            if (bike != null && !bike.Active)
            {
                bike.Active = true;
                await _bikeService.UpdateBikeAsync(bike);
            }

            return NoContent();
        }

        private bool BikeExists(int id)
        {
            return _bikeService.GetBikeAsync(id) != null;
        }

        private bool BrandExists(int id)
        {
            return _bikeService.GetBrandByIdAsync(id) != null;
        }
        private bool ModelExists(int id)
        {
            return _bikeService.GetModelByIdAsync(id) != null;
        }
    }
}
