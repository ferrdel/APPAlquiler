using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public ModelsController(AlquilerDbContext context)
        {
            _context = context;
        }

        //GET: api/Modelos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelos= _context.Models.ToListAsync();
            return Ok(modelos);
        }

        [HttpGet("{id}", Name = "GetModelo")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model=await _context.Models.FindAsync(id);

            if(model == null)
            {
                return NotFound("Modelo not found");
            }
            else
            {
                return Ok(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostModelos([FromBody] ModelDto modeloDto) 
        {
            if (ModelState.IsValid)
            {
                var model = new Model
                {
                    Name = modeloDto.Name,
                    State = modeloDto.State
                };

                _context.Models.Add(model);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetModelo",new {id= model.IdModel},model);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //DELETE: api/action/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelo(int id) 
        {
            var modelo = await _context.Models.FindAsync(id);

            if(modelo != null)
            {
                modelo.State = true;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPut]
        public async Task RestoreModelo(int id)
        {
            var producto = await _context.Models.FindAsync(id);

            if (producto != null && producto.State)
            {
                producto.State = false;
                await _context.SaveChangesAsync();
            }
        }


        private bool ModelExists(int id)
        {
            return _context.Models.Any(m => m.IdModel == id);
        }
    }
}
