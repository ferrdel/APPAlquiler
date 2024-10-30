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
    public class BrandsController : ControllerBase
    {
        private readonly AlquilerDbContext _context;

        public BrandsController(AlquilerDbContext context)
        {
            _context = context;
        }

        //GET: api/Brand
        [HttpGet]
        public async Task<IActionResult> GetAllBrand() //obtener todas las marcas
        {
            var brand = _context.Brands.ToListAsync();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> PostBrand([FromBody] BrandDto brandDto)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    Name = brandDto.Name,
                    State = brandDto.State
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBrand", new { id = brand.IdBrand }, brand);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //Metodo eliminar, cambiando a false el estado de la marca

        [HttpDelete("{id}", Name = "DeleteBrand")]
        public async Task DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand != null && brand.State)
            {
                brand.State = false;
                await _context.SaveChangesAsync();
            }
        }

        private bool ModelExists(int id)
        {
            return _context.Brands.Any(m => m.IdBrand == id);
        }

    }
}
