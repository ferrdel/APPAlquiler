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
            var brand = await _context.Brands.ToListAsync();
            return Ok(brand);
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound("Brand not found");
            }

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
                    Active = brandDto.Active
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
        }

        //modificar brand
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BrandExists(id))
                {
                    return NotFound("Brand not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        //Metodo eliminar, cambiando a false el estado de la marca

        [HttpDelete("{id}", Name = "DeleteBrand")]
        public async Task DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand != null && brand.Active)
            {
                brand.Active = false;
                await _context.SaveChangesAsync();
            }
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(m => m.Id == id);
        }

    }
}
