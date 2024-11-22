using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        //GET: api/Modelos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetAllModels()
        {
            var succeeded = await _modelService.GetAllModelAsync();
            var modelDetails = succeeded.Select(model => new ModelDetailsDto
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                Brand = model.Brand.Name
            });
            return Ok(modelDetails);
        }

        [HttpGet("{id}", Name = "GetModel")]
        public async Task<ActionResult<ModelDto>> GetModel(int id)
        {
            var model = await _modelService.GetModelAsync(id);

            if(model == null)
            {
                return NotFound("Model not found");
            }

            var modelDto = new ModelDto
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active,
                BrandId = model.BrandId,
            };

            return modelDto;
        }


        [HttpPut("{id}", Name = "PutModel")]
        public async Task<IActionResult> PutModel(int id, [FromBody] ModelDto modelDto)
        {
            if (id != modelDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            //Verificacion de que existe la marca
            if (!BrandExists(modelDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand Id Not found.");
                return BadRequest(ModelState);
            }

            var model = await _modelService.GetModelAsync(id);
            if (model == null)
                return NotFound("Model not found");
            if (model.Active != modelDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            model.Name = modelDto.Name;
            model.BrandId = modelDto.BrandId;

            var succeeded = await _modelService.UpdateModelAsync(model);
            if (!succeeded) return BadRequest("fallo");
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostModel([FromBody] ModelDto modelDto) 
        {
            //Verificacion de que existe la marca
            if (!BrandExists(modelDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand Id Not found.");
                return BadRequest(ModelState);
            }

            try
            { 
                var model = new Model
                {
                    Name = modelDto.Name,
                    Active = modelDto.Active,
                    BrandId = modelDto.BrandId
                };

                var succeeded = await _modelService.AddModelAsync(model);
                if (succeeded)
                    return CreatedAtAction("GetModel", new { Id = model.Id }, model);
                else
                    return BadRequest("Failed to create");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/action/2

        //(No se si es la forma correcta  de realizar el delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id) 
        {
            var model = await _modelService.GetModelAsync(id);

            if(model != null && model.Active)
            {
                model.Active = false;
                await _modelService.UpdateModelAsync(model);    //Cambia el estado Active a falso (baja logica).
            }
            else return BadRequest("Not found model or not Active");

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActivateModel(int id)
        {
            var model = await _modelService.GetModelAsync(id);
            if (model != null && !model.Active)
            {
                model.Active = true;
                await _modelService.UpdateModelAsync(model);
            }
            else return BadRequest("Not found model or Active");

            return NoContent();
        }

        private bool ModelExists(int id)
        {
            return _modelService.GetModelAsync(id) != null;
        }
        private bool BrandExists(int id)
        {
            var exists = _modelService.GetBrandByIdAsync(id).Result;
            return exists != null;
        }
    }
}
