﻿using AppAlquiler_BusinessLayer.Interfaces;
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
        public async Task<IActionResult> GetAllModels()
        {
            var model= await _modelService.GetAllModelAsync();
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetModel")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model=await _modelService.GetModelAsync(id);

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


        [HttpPut("{id}", Name = "PutModel")]
        public async Task<IActionResult> PutModel(int id, [FromBody] Model model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id mismatch");
            }

            //_context.Entry(model).State = EntityState.Modified;

            try
            {
                var succeeded = await _modelService.UpdateModelAsync(model);
                if (succeeded)
                    return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ModelExists(id))
                {
                    return NotFound("Model not found");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
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

            return NoContent();
        }

        //Metodo de Activar el modelo modificando el estado
        /*
        [HttpPut("{id}")]
        public async Task RestoreModel(int id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model != null && model.State)
            {
                model.State = false;
                await _context.SaveChangesAsync();
            }
        }
        */

        private bool ModelExists(int id)
        {
            return _modelService.GetModelAsync(id) != null;
        }
    }
}
