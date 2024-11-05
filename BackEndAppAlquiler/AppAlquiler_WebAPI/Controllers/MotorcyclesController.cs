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
using AppAlquiler_BusinessLayer.Interfaces;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcyclesController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        // GET: api/Motorcycles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorcycle>>> GetMotorcycles()
        {
            var motorcycle = await _motorcycleService.GetAllMotorcycleAsync();
            return Ok(motorcycle);
        }

        // GET: api/Motorcycles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorcycle>> GetMotorcycle(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);

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
                return BadRequest("Id mismatch");
            }

            try
            {
                var succeeded = await _motorcycleService.UpdateMotorcycleAsync(motorcycle);
                if (succeeded)
                    return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MotorcycleExists(id))
                {
                    return NotFound("Motorcycle not found");
                }
                else
                {
                    return BadRequest(ex.Message);
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
                //Verificacion de que existe la marca
                if (!BrandExists(motorcycleDto.BrandId))
                {
                    ModelState.AddModelError("BrandId", "Brand Id Not found.");
                    return BadRequest(ModelState);
                }

                //Verificacion de que existe el modelo
                if (!ModelExists(motorcycleDto.ModelId))
                {
                    ModelState.AddModelError("ModelId", "Model Id Not found.");
                    return BadRequest(ModelState);
                }

                //Verificacion de que existe el tipo de motocicleta
                if (!TypeExists(motorcycleDto.TypeId))
                {
                    ModelState.AddModelError("TypeId", "Type Id Not found.");
                    return BadRequest(ModelState);
                }

                var motorcycle = new Motorcycle
                {
                    Description = motorcycleDto.Description,
                    GasolineConsumption = motorcycleDto.GasolineConsumption,
                    LuggageCapacity = motorcycleDto.LuggageCapacity,
                    PassengerCapacity = motorcycleDto.PassengerCapacity,
                    Fuel = motorcycleDto.Fuel,
                    State = motorcycleDto.State,
                    Active = motorcycleDto.Active,
                    Price = motorcycleDto.Price,
                    ModelID = motorcycleDto.ModelId,
                    BrandId = motorcycleDto.BrandId,
                    
                    Abs=motorcycleDto.Abs,
                    cilindrada=motorcycleDto.cilindrada,
                    TypeId=motorcycleDto.TypeId
                };

                var succeeded = await _motorcycleService.AddMotorcycleAsync(motorcycle);
                if (succeeded)
                    return CreatedAtAction("GetMotorcycle", new { Id = motorcycle.Id }, motorcycle);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Motorcycles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycle(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);
            if (motorcycle == null)
            {
                return NotFound();
            }
            motorcycle.Active = false; //modifica el state a true para poder eliminarlo

            await _motorcycleService.DeleteMotorcycleAsync(motorcycle.Id);

            return NoContent();
        }

        private bool MotorcycleExists(int id)
        {
            return _motorcycleService.GetMotorcycleAsync(id) != null;
        }
        private bool BrandExists(int id)
        {
            return _motorcycleService.GetBrandByIdAsync(id) != null;
        }
        private bool ModelExists(int id)
        {
            return _motorcycleService.GetModelByIdAsync(id) != null;
        }
        private bool TypeExists(int id)
        {
            return _motorcycleService.GetTypeMotorcycleAsync(id) != null;
        }
    }
}
