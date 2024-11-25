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

        [HttpGet]
        public async Task<IActionResult> GetRents()
        {
            var rents = await _rentService.GetAllRentAsync();
            return Ok(rents);
        }

        [HttpGet("UserRents")]
        public async Task<ActionResult<CreateRentDto>> GetRentsByUserAsync(int id)
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
            var rent = await _rentService.GetRentDtoByIdAsync(id);
            return Ok(rent);
        }

        [HttpGet("MyRents")]
        public async Task<ActionResult<MyRentDto>> GetMyRentsAsync()
        {
            var rents = await _rentService.GetMyRentsAsync();

            if (rents == null)
            {
                return NotFound("Rents Not found");
            }
            return Ok(rents);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRent(int id, [FromBody] UpdateRentDto rentDto)
        {
            if (id != rentDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            if (!RentExists(id))
            {
                return NotFound("Rent not found");
            }

            var rent = await _rentService.GetByIdAsync(id);
            if (rent == null)
                return NotFound("rent not found");
            
            rent.PickUpDate = rentDto.PickUpDate;
            rent.ReturnDate = rentDto.ReturnDate;
            rent.PickUpTime = rentDto.PickUpTime;
            rent.ReturnTime = rentDto.ReturnTime;
            rent.State = Enum.Parse<RentState>(rentDto.State);
            rent.Vehicle = Enum.Parse<TypeVehicle>(rentDto.Vehicle);

            var succeeded = await _rentService.UpdateRentAsync(rent);
            if (!succeeded) return BadRequest("fallo");
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRent([FromBody] CreateRentDto rentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //TODO: Añadir más validaciones, que el userId sea mayor a cero, etc.
            //TODO: Map CreateRentDto to RentDto

            try
            {
                var rent = new Rent
                {

                    PickUpDate = rentDto.PickUpDate,
                    ReturnDate = rentDto.ReturnDate,
                    PickUpTime = rentDto.PickUpTime,
                    ReturnTime = rentDto.ReturnTime,
                    State = Enum.Parse<RentState>(rentDto.State),          //Se puede definir automaticamente como Pendiente
                    Vehicle = Enum.Parse<TypeVehicle>(rentDto.Vehicle),

                    VehicleId = rentDto.VehicleId,
                    UserId = rentDto.UserId
                };

                var succeded = await _rentService.PlaceRentAsync(rent);
                if (succeded) return Created();
                else return BadRequest("Rent could not be placed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool RentExists(int id)
        {
            var exists = _rentService.GetByIdAsync(id).Result;
            return exists != null;
        }
    }
}
