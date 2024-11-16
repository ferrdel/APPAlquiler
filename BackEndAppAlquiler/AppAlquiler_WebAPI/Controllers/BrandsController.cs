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
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrand() //obtener todas las marcas
        {
            var brand = await _brandService.GetAllBrandAsync();
            return Ok(brand);
        }

        [HttpGet("{id}", Name = "GetBrand")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brand = await _brandService.GetBrandAsync(id);

            if (brand == null)
            {
                return NotFound("Brand not found");
            }

            var brandDto = new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Active = brand.Active
            };

            return Ok(brand);
        }

        //modificar brand
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, [FromBody] BrandDto brandDto)
        {
            if (id != brandDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var brand = await _brandService.GetBrandAsync(id);
            if (brand == null)
                return NotFound("Brand not found");
            if (brand.Active != brandDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            brand.Name = brandDto.Name;

            var succeeded = await _brandService.UpdateBrandAsync(brand);
            if (!succeeded) return BadRequest("fallo");
            return Ok("Succeeded");
        }

        [HttpPost]
        public async Task<IActionResult> PostBrand([FromBody] BrandDto brandDto)
        {
            try
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
                    return BadRequest("Failed to create");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Metodo eliminar, cambiando a false el estado de la marca

        [HttpDelete("{id}", Name = "DeleteBrand")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.GetBrandAsync(id);

            if (brand != null && brand.Active)
            {
                brand.Active = false;
                await _brandService.UpdateBrandAsync(brand);    //Cambia el estado Active a falso (baja logica).
            }
            else return BadRequest("Not found brand or not Active");

            return Ok("Logical delete was successful.");

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateBrand(int id)
        {
            var brand = await _brandService.GetBrandAsync(id);
            if (brand != null && !brand.Active)
            {
                brand.Active = true;
                await _brandService.UpdateBrandAsync(brand);
            }
            else return BadRequest("Not found brand or Active");

            return Ok("Logical activation was successful.");
        }

        private bool BrandExists(int id)
        {
            return _brandService.GetBrandAsync(id) != null;
        }

    }
}
