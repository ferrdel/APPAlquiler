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
            var model= await _modelService.GetAllModelAsync();
            return Ok(model);
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

            var model = await _modelService.GetModelAsync(id);
            if (model == null)
                return NotFound("Model not found");
            if (model.Active != modelDto.Active)         //Verifica que no se cambia el valor de Active en la funcion update
                return BadRequest("The active attribute cannot be changed in this option.");

            model.Name = modelDto.Name;

            var succeeded = await _modelService.UpdateModelAsync(model);
            if (!succeeded) return BadRequest("fallo");
            return Ok("Succeeded");
        }

        [HttpPost]
        public async Task<IActionResult> PostModel([FromBody] ModelDto modelDto) 
        {
            if (ModelState.IsValid)
            {
                var model = new Model
                {
                    Name = modelDto.Name,
                    Active = modelDto.Active
                };

                var succeeded = await _modelService.AddModelAsync(model);
                if (succeeded)
                    return CreatedAtAction("GetModel", new { Id = model.Id }, model);
                else
                    return BadRequest(ModelState);
            }
            return BadRequest(ModelState); // elModelState es la representacion del modelo
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

            return Ok("Logical delete was successful.");
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

            return Ok("Logical activation was successful.");
        }

        private bool ModelExists(int id)
        {
            return _modelService.GetModelAsync(id) != null;
        }
    }
}
