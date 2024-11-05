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
    public class BoatsController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public BoatsController(AlquilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Boats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boat>>> GetBoats()
        {
            return await _context.Boats.ToListAsync();
        }

        // GET: api/Boats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boat>> GetBoat(int id)
        {
            var boat = await _context.Boats.FindAsync(id);

            if (boat == null)
            {
                return NotFound();
            }

            return boat;
        }

        // PUT: api/Boats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoat(int id, Boat boat)
        {
            if (id != boat.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(boat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
                var brandExists = await _context.Brands.AnyAsync(b => b.Id == boatDto.BrandId);
                if (!brandExists)
                {
                    ModelState.AddModelError("BrandId", "Brand Id Not found.");
                    return BadRequest(ModelState);
                }

                //Verificacion de que existe el modelo
                var modelExists = await _context.Models.AnyAsync(b => b.Id == boatDto.ModelId);
                if (!modelExists)
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
                    State = boatDto.State,
                    Active = boatDto.Active,
                    Price = boatDto.Price,
                    ModelID = boatDto.ModelId,
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

                _context.Boats.Add(boat);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetBoat", new { Id = boat.Id }, boat);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        // DELETE: api/Boats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoat(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
            {
                return NotFound();
            }
            boat.Active =false; //cambia el estado a true para que quede como eliminado

            _context.Boats.Update(boat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoatExists(int id)
        {
            return _context.Boats.Any(e => e.Id == id);
        }
    }
}
