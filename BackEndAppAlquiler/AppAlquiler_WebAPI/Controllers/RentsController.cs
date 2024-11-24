using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAlquiler_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IRentService _rentService;
        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRent([FromBody] CreateRentDto rentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //TODO: Añadir más validaciones, que el userId sea mayor a cero, etc.
            //TODO: Map CreateRentDto to RentDto


            try
            {
                var rent = new RentDto
                {

                    PickUpDate = rentDto.PickUpDate,
                    ReturnDate = rentDto.ReturnDate,
                    PickUpTime = rentDto.PickUpTime,
                    ReturnTime = rentDto.ReturnTime,
                    State = Enum.Parse<RentState>(rentDto.State).ToString(),          //Se puede definir automaticamente como Pendiente
                    Vehicle = Enum.Parse<TypeVehicle>(rentDto.Vehicle).ToString(),

                    VehicleId = rentDto.VehicleId,
                    UserId = rentDto.UserId
                };

                var succeded = await _rentService.PlaceRentAsync(rent);
                if (succeded) return Ok("Rent placed successfully");
                else return BadRequest("Rent could not be placed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRents()
        {
            var rents = await _rentService.GetAllRentAsync();
            return Ok(rents);
        }
        
        [HttpGet("MyRents")]
        public async Task<ActionResult<RentDto>> GetMyRentsAsync()
        {
            var rents = await _rentService.GetMyRentsAsync();

            if (rents == null)
            {
                return NotFound("Rents Not found");
            }
            return Ok(rents);
        }

        [HttpGet("UserRents")]
        public async Task<ActionResult<RentDto>> GetRentsByUserAsync(int id)
        {
            var rents = await _rentService.GetRentsByUserIdAsync(id);

            if (rents == null)
            {
                return NotFound("Rents Not found");
            }
            return Ok(rents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentDto>> GetRentByidAsync(int id)
        {
            var rent = await _rentService.GetByIdAsync(id);
            return Ok(rent);
        }
        
    }
}
