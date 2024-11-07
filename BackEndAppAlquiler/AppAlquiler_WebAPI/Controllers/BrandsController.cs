using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
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

        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //GET: api/Brand
        [HttpGet]
        public async Task<IActionResult> GetAllBrand() //obtener todas las marcas
        {
            var brand = await _brandService.GetAllBrandAsync();
            return Ok(brand);
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _brandService.GetBrandAsync(id);

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

                var succeeded = await _brandService.AddBrandAsync(brand);
                if (succeeded)
                    return CreatedAtAction("GetBrand", new { Id = brand.Id }, brand);
                else
                    return BadRequest(ModelState);

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
            //_context.Entry(brand).State = EntityState.Modified;

            try
            {
                var succeeded = await _brandService.UpdateBrandAsync(brand);
                if (succeeded)
                    return NoContent();
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
            var brand = await _brandService.GetBrandAsync(id);

            if (brand != null && brand.Active)
            {
                brand.Active = false;
                await _brandService.UpdateBrandAsync(brand);    //Cambia el estado Active a falso (baja logica).
            }

        }

        private bool BrandExists(int id)
        {
            return _brandService.GetBrandAsync(id) != null;
        }

    }
}
