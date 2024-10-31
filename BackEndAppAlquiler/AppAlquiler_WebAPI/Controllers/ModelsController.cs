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
        public async Task<IActionResult> GetAllModels()
        {
            var model= _context.Models.ToListAsync();
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetModel")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model=await _context.Models.FindAsync(id);

            if(model == null)
            {
                return NotFound("Model not found");
            }
            else
            {
                return Ok(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostModel([FromBody] ModelDto modelDto) 
        {
            if (ModelState.IsValid)
            {
                var model = new Model
                {
                    Name = modelDto.Name,
                    State = modelDto.State
                };

                _context.Models.Add(model);
                await _context.SaveChangesAsync(); //guarda los cambios
                return CreatedAtAction("GetModelo",new {id= model.Id},model);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //DELETE: api/action/2

        //(No se si es la forma correcta  de realizar el delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id) 
        {
            var model = await _context.Models.FindAsync(id);

            if(model != null)
            {
                model.State = true;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        //Metodo de delete modificando el estado
        [HttpPut("{id}", Name = "DeleteModel")]
        public async Task RestoreModel(int id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model != null && model.State)
            {
                model.State = false;
                await _context.SaveChangesAsync();
            }
        }


        private bool ModelExists(int id)
        {
            return _context.Models.Any(m => m.Id == id);
        }
    }
}
