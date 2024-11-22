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
using System.Runtime.ConstrainedExecution;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IBoatService _boatService;

        public BoatsController(IBoatService boatService)
        {
            _boatService = boatService;
        }

        // GET: api/Boats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoatDetailsDto>>> GetBoats()
        {
            var succeded = await _boatService.GetAllBoatAsync();
            var boatDetails = succeded.Select(boat => new BoatDetailsDto
            {
                Id = boat.Id,    //agregado para el front
                Description = boat.Description,
                GasolineConsumption = boat.GasolineConsumption,
                LuggageCapacity = boat.LuggageCapacity,
                PassengerCapacity = boat.PassengerCapacity,
                Fuel = boat .Fuel,

                //parseo
                State = Enum.GetName(boat.State),
                Active = boat.Active,
                Price = boat.Price,
                Image = boat.Image,
                Model = boat.Model.Name,
                Brand = boat.Model.Brand.Name,

                Dimension = boat.Dimension,
                Engine = boat.Engine,
                Material = boat.Material,
                Stability = boat.Stability,
                Navigation = boat.Navigation,
                Facilities = boat.Facilities,
                Sound = boat.Sound,
                Accessories = boat.Accessories,
                Propulsion = boat.Propulsion

            });
            return Ok(boatDetails);
        }

        // GET: api/Boats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoatDto>> GetBoat(int id)
        {
            var boat = await _boatService.GetBoatAsync(id);

            if (boat == null)
            {
                return NotFound("Boat not found");
            }

            //Agregado devolver boatDTO
            var boatDto = new BoatDto
            {
                Id = boat.Id,    //agregado para el front
                Description = boat.Description,
                GasolineConsumption = boat.GasolineConsumption,
                LuggageCapacity = boat.LuggageCapacity,
                PassengerCapacity = boat.PassengerCapacity,
                Fuel = boat.Fuel,

                //parseo
                State = Enum.GetName(boat.State),
                Active = boat.Active,
                Price = boat.Price,
                Image = boat.Image,
                ModelId = boat.ModelId,
                BrandId = boat.Model.BrandId,
                //CAracteristicas de Boat
                Dimension = boat.Dimension,
                Engine = boat.Engine,
                Material = boat.Material,
                Stability = boat.Stability,
                Navigation = boat.Navigation,
                Facilities = boat.Facilities,
                Sound = boat.Sound,
                Accessories = boat.Accessories,
                Propulsion = boat.Propulsion
            };

            return boatDto;
        }

        // PUT: api/Boats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoat(int id,[FromBody] BoatDto boatDto)
        {
            if (id != boatDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            if (!BoatExists(id))
            {
                return NotFound("boat not found");
            }

            //Verificacion de que existe el modelo
            if (!ModelExists(boatDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            var boat = await _boatService.GetBoatAsync(id);
            if (boat == null)
                return NotFound("Boat not found");
            if (boat.Active != boatDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            boat.Description = boatDto.Description;
            boat.GasolineConsumption = boatDto.GasolineConsumption;
            boat.LuggageCapacity = boatDto.LuggageCapacity;
            boat.PassengerCapacity = boatDto.PassengerCapacity;
            boat.Fuel = boatDto.Fuel;
            boat.State = Enum.Parse<State>(boatDto.State);
            boat.Price = boatDto.Price;
            boat.Image = boatDto.Image;
            boat.ModelId = boatDto.ModelId;
            //caracteristicas de la lancha
            boat.Dimension = boatDto.Dimension;
            boat.Engine = boatDto.Engine;
            boat.Material = boatDto.Material;
            boat.Stability = boatDto.Stability;
            boat.Navigation = boatDto.Navigation;
            boat.Facilities = boatDto.Facilities;
            boat.Sound = boatDto.Sound;
            boat.Accessories = boatDto.Accessories;
            boat.Propulsion = boatDto.Propulsion;

            var succeeded = await _boatService.UpdateBoatAsync(boat);
            if (!succeeded) return BadRequest("Fallo"); 
            return NoContent();
        }

        // POST: api/Boats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBoat([FromBody ]BoatDto boatDto)
        {
            //Verificacion de que existe el modelo
            if (!ModelExists(boatDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            try
            {
                var boat = new Boat
                {
                    Description = boatDto.Description,
                    GasolineConsumption = boatDto.GasolineConsumption,
                    LuggageCapacity = boatDto.LuggageCapacity,
                    PassengerCapacity = boatDto.PassengerCapacity,
                    Fuel = boatDto.Fuel,
                    State = Enum.Parse<State>(boatDto.State),
                    Active = boatDto.Active,
                    Price = boatDto.Price,
                    Image = boatDto.Image,
                    ModelId = boatDto.ModelId,
                    //caracteristicas de la lancha
                    Dimension = boatDto.Dimension,
                    Engine = boatDto.Engine,
                    Material = boatDto.Material,
                    Stability = boatDto.Stability,
                    Navigation = boatDto.Navigation,
                    Facilities = boatDto.Facilities,
                    Sound = boatDto.Sound,
                    Accessories = boatDto.Accessories,
                    Propulsion = boatDto.Propulsion
                };

                var succeeded = await _boatService.AddBoatAsync(boat);
                if (succeeded)
                    return CreatedAtAction("GetBoat", new { Id = boat.Id }, boat);
                else
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Boats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoat(int id)
        {
            var boat = await _boatService.GetBoatAsync(id);
            if (boat != null)
            {
                boat.Active = false; //cambia el estado a true para que quede como eliminado
                await _boatService.UpdateBoatAsync(boat);
            }
            else return BadRequest("Not found boat or not active");

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateBoat(int id)
        {
            var boat = await _boatService.GetBoatAsync(id);
            if (boat != null && !boat.Active)
            {
                boat.Active = true;
                await _boatService.UpdateBoatAsync(boat);
            }
            else return BadRequest("Not found boat or Active");

            return NoContent();
        }

        private bool BoatExists(int id)
        {
            var exists = _boatService.GetBoatAsync(id).Result;
            return exists != null;
        }

        private bool ModelExists(int id)
        {
            var exists = _boatService.GetModelByIdAsync(id).Result;  
            return exists != null;
        }
    }
}
