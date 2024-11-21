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
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public async Task<ActionResult<IEnumerable<BikeDetailsDto>>> GetBikes()
        {
            var succeded = await _bikeService.GetAllBikeAsync();
            var bikeDetails = succeded.Select(bike => new BikeDetailsDto
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
                Image = bike.Image,
                Model = bike.Model.Name,

                //agregado brand
                Brand = bike.Model.Brand.Name,
                //Caracteristicas Bike
                Whell = bike.Whell,
                FrameSize = bike.FrameSize,
                NumberSpeeds = bike.NumberSpeeds
            });
            return Ok(bikeDetails);
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

            //var model = _bikeService.GetModelByIdAsync(bike.ModelId).Result;        //Llamamos a model sin ninguna verificacion porque ya sabemos que existe.
            //Necesita un nuevo Dto (BikeFrontDto)
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
                Image = bike.Image,
                ModelId = bike.ModelId,
                BrandId = bike.Model.BrandId,
                //Caracteristicas Bike
                Whell = bike.Whell,
                FrameSize = bike.FrameSize,
                NumberSpeeds = bike.NumberSpeeds
            };

            return bikeDTO;
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
            if (!BikeExists(id))
            {
                return NotFound("Bike not found");
            }
            //Verificacion de que existe el modelo
            if (!ModelExists(bikeDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            var bike = await _bikeService.GetBikeAsync(id);
            if (bike == null)
                return NotFound("bike not found");
            if (bike.Active != bikeDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            bike.Description = bikeDto.Description;
            bike.GasolineConsumption = bikeDto.GasolineConsumption;
            bike.LuggageCapacity = bikeDto.LuggageCapacity;
            bike.PassengerCapacity = bikeDto.PassengerCapacity;
            bike.Fuel = bikeDto.Fuel;
            bike.State = Enum.Parse<State>(bikeDto.State.ToLower());
            bike.Price = bikeDto.Price;
            bike.Image = bikeDto.Image;
            bike.ModelId = bikeDto.ModelId;

            bike.Whell = bikeDto.Whell;
            bike.FrameSize = bikeDto.FrameSize;
            bike.NumberSpeeds = bikeDto.NumberSpeeds;

            var succeeded = await _bikeService.UpdateBikeAsync(bike);
            if (!succeeded) return BadRequest("Fallo");
            return NoContent();
        }

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBike([FromBody] BikeDto bikeDto)
        {
            //Verificacion de que existe el modelo
            if (!ModelExists(bikeDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

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
                    Image = bikeDto.Image,
                    ModelId = bikeDto.ModelId,
                    Whell = bikeDto.Whell,
                    FrameSize = bikeDto.FrameSize,
                    NumberSpeeds = bikeDto.NumberSpeeds
                };

                var succeeded = await _bikeService.AddBikeAsync(bike);
                if (succeeded)
                    return CreatedAtAction("GetBike", new { Id = bike.Id }, bike);
                else
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            else return BadRequest("Not found bike or not Active");

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
            else return BadRequest("Not found bike or Active");

            return NoContent();
        }

        private bool BikeExists(int id)
        {
            var exists = _bikeService.GetBikeAsync(id).Result;
            return exists != null;
        }

        private bool ModelExists(int id)
        {
            var exists = _bikeService.GetModelByIdAsync(id).Result;
            return exists != null;
        }
    }
}
