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
using AppAlquiler_BusinessLayer.Services;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<MotorcycleDto>>> GetMotorcycles()
        {
            var succeded = await _motorcycleService.GetAllMotorcycleAsync();
            var motorcycleDetails = succeded.Select(motorcycle => new MotorcycleDetailsDto
            {
                Id = motorcycle.Id,    //agregado para el front
                Description = motorcycle.Description,
                GasolineConsumption = motorcycle.GasolineConsumption,
                LuggageCapacity = motorcycle.LuggageCapacity,
                PassengerCapacity = motorcycle.PassengerCapacity,
                Fuel = motorcycle.Fuel,

                //parseo
                State = Enum.GetName(motorcycle.State),
                Active = motorcycle.Active,
                Price = motorcycle.Price,
                Image = motorcycle.Image,
                Model = motorcycle.Model.Name,
                Brand = motorcycle.Model.Brand.Name,

                Abs = motorcycle.Abs,
                Cilindrada = motorcycle.Cilindrada,
                TypeMotorcycle = Enum.GetName(motorcycle.TypeMotorcycle.Name)                       //verificar
            });
            return Ok(motorcycleDetails);
        }

        // GET: api/Motorcycles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MotorcycleDto>> GetMotorcycle(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);

            if (motorcycle == null)
            {
                return NotFound("Motorcycle not found");
            }

            var motorcycleDto = new MotorcycleDto
            {
                Id = motorcycle.Id,
                Description = motorcycle.Description,
                GasolineConsumption = motorcycle.GasolineConsumption,
                LuggageCapacity = motorcycle.LuggageCapacity,
                PassengerCapacity = motorcycle.PassengerCapacity,
                Fuel = motorcycle.Fuel,

                State = Enum.GetName(motorcycle.State),
                Active = motorcycle.Active,
                Price = motorcycle.Price,
                Image = motorcycle.Image,
                ModelId = motorcycle.ModelId,
                BrandId = motorcycle.Model.BrandId,

                Abs = motorcycle.Abs,
                Cilindrada = motorcycle.Cilindrada,
                TypeMotorcycleId = motorcycle.TypeMotorcycleId
            };

            return motorcycleDto;
        }

        // PUT: api/Motorcycles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutMotorcycle(int id, MotorcycleDto motorcycleDto)
        {
            if (id != motorcycleDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            if (!MotorcycleExists(id))
            {
                return NotFound("Motorcycle not found");
            }
            //Verificacion de que existe el modelo
            if (!ModelExists(motorcycleDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }
            //Verificacion de que existe el tipo de motocicleta
            if (!TypeMotorcycleExists(motorcycleDto.TypeMotorcycleId))
            {
                ModelState.AddModelError("TypeId", "Type Id Not found.");
                return BadRequest(ModelState);
            }

            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);
            if (motorcycle == null)
                return NotFound("motorcycle not found");
            
            if (motorcycle.Active != motorcycleDto.Active)      //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            motorcycle.Description = motorcycleDto.Description;
            motorcycle.GasolineConsumption = motorcycleDto.GasolineConsumption;
            motorcycle.LuggageCapacity = motorcycleDto.LuggageCapacity;
            motorcycle.PassengerCapacity = motorcycleDto.PassengerCapacity;
            motorcycle.Fuel = motorcycleDto.Fuel;
            motorcycle.State = Enum.Parse<State>(motorcycleDto.State);
            motorcycle.Price = motorcycleDto.Price;
            motorcycle.Image = motorcycleDto.Image;
            motorcycle.ModelId = motorcycleDto.ModelId;

            motorcycle.Abs = motorcycleDto.Abs;
            motorcycle.Cilindrada = motorcycleDto.Cilindrada;
            motorcycle.TypeMotorcycleId = motorcycleDto.TypeMotorcycleId;

            var succeeded = await _motorcycleService.UpdateMotorcycleAsync(motorcycle);
            if (!succeeded) return BadRequest("fallo");
            return NoContent();
        }

        // POST: api/Motorcycles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostMotorcycle([FromBody]MotorcycleDto motorcycleDto)
        {
            //Verificacion de que existe el modelo
            if (!ModelExists(motorcycleDto.ModelId))
            {
                ModelState.AddModelError("ModelId", "Model Id Not found.");
                return BadRequest(ModelState);
            }

            //Verificacion de que existe el tipo de motocicleta
            if (!TypeMotorcycleExists(motorcycleDto.TypeMotorcycleId))
            {
                ModelState.AddModelError("TypeId", "Type Id Not found.");
                return BadRequest(ModelState);
            }

            try
            { 
                var motorcycle = new Motorcycle
                {
                    Description = motorcycleDto.Description,
                    GasolineConsumption = motorcycleDto.GasolineConsumption,
                    LuggageCapacity = motorcycleDto.LuggageCapacity,
                    PassengerCapacity = motorcycleDto.PassengerCapacity,
                    Fuel = motorcycleDto.Fuel,
                    State = Enum.Parse<State>(motorcycleDto.State),
                    Active = motorcycleDto.Active,
                    Price = motorcycleDto.Price,
                    Image = motorcycleDto.Image,
                    ModelId = motorcycleDto.ModelId,
                    
                    Abs=motorcycleDto.Abs,
                    Cilindrada=motorcycleDto.Cilindrada,
                    TypeMotorcycleId=motorcycleDto.TypeMotorcycleId
                };

                var succeeded = await _motorcycleService.AddMotorcycleAsync(motorcycle);
                if (succeeded)
                    return CreatedAtAction("GetMotorcycle", new { Id = motorcycle.Id }, motorcycle);
                else
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Motorcycles/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteMotorcycle(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);
            if (motorcycle != null && motorcycle.Active)
            {
                motorcycle.Active = false;
                await _motorcycleService.UpdateMotorcycleAsync(motorcycle);    //Cambia el estado Active a falso (baja logica).
            }
            else return BadRequest("Not found motorcycle or not Active");

            return NoContent();
        }

        [HttpPatch("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ActivateMotorcycle(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleAsync(id);
            if (motorcycle != null && !motorcycle.Active)
            {
                motorcycle.Active = true;
                await _motorcycleService.UpdateMotorcycleAsync(motorcycle);
            }
            else return BadRequest("Not found motorcycle or Active");

            return NoContent();
        }

        private bool MotorcycleExists(int id)
        {
            var exists = _motorcycleService.GetMotorcycleAsync(id).Result;
            return exists != null;
        }
        private bool ModelExists(int id)
        {
            var exists = _motorcycleService.GetModelByIdAsync(id).Result;
            return exists != null;
        }

        private bool TypeMotorcycleExists(int id)
        {
            var exists = _motorcycleService.GetTypeMotorcycleByIdAsync(id).Result;
            return exists != null;
        }
    }
}
