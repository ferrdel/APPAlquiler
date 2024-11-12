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
        public async Task<ActionResult<IEnumerable<BoatDto>>> GetBoats()
        {
            var succeeded = await _boatService.GetAllBoatAsync();
            return Ok(succeeded);
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
                ModelId = boat.ModelId,
                BrandId = boat.BrandId,
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
        public async Task<IActionResult> PutBoat(int id, BoatDto boatDto)
        {
            if (id != boatDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var boat = new Boat
            {
                Id = (int)boatDto.Id, //agregado porque no llegaba id. Ademas castea porque podia es nullable
                Description = boatDto.Description,
                GasolineConsumption = boatDto.GasolineConsumption,
                LuggageCapacity = boatDto.LuggageCapacity,
                PassengerCapacity = boatDto.PassengerCapacity,
                Fuel = boatDto.Fuel,
                State = Enum.Parse<State>(boatDto.State),
                Active = boatDto.Active,
                Price = boatDto.Price,
                ModelId = boatDto.ModelId,
                BrandId = boatDto.BrandId,
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

            try
            {
                var succeeded = await _boatService.UpdateBoatAsync(boat);
                if (succeeded) NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BoatExists(id))
                {
                    return NotFound("boat not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Boats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBoat([FromBody ]BoatDto boatDto)
        {
            if (ModelState.IsValid)
            {
                //Verificacion de que existe la marca
                if (!BrandExists(boatDto.BrandId))
                {
                    ModelState.AddModelError("BrandId", "Brand Id Not found.");
                    return BadRequest(ModelState);
                }

                //Verificacion de que existe el modelo
                if (!ModelExists(boatDto.ModelId))
                {
                    ModelState.AddModelError("ModelId", "Model Id Not found.");
                    return BadRequest(ModelState);
                }

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
                    ModelId = boatDto.ModelId,
                    BrandId = boatDto.BrandId,
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
                    return BadRequest(ModelState);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
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

            return NoContent();
        }

        private bool BoatExists(int id)
        {
            return _boatService.GetBoatAsync(id) != null;
        }

        private bool BrandExists(int id)
        {
            return _boatService.GetBrandByIdAsync(id) != null;
        }
        private bool ModelExists(int id)
        {
            return _boatService.GetModelByIdAsync(id) != null;
        }
    }
}
