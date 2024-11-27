using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_WebAPI.Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetRents()
        {
            var rents = await _rentService.GetAllRentAsync();
            return Ok(rents);
        }

        [HttpGet("UserRents")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<MyRentDto>> GetRentsByUserAsync(int id)
        {
            var rents = await _rentService.GetRentsByUserIdAsync(id);

            if (rents == null)
            {
                return NotFound("Rents Not found");
            }
            return Ok(rents);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<RentDto>> GetRentByidAsync(int id)
        {
            var rent = await _rentService.GetRentDtoByIdAsync(id);
            return Ok(rent);
        }

        [HttpGet("MyRents")]
        //[Authorize]
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
        //[Authorize(Roles = "ADMIN")]
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
        //[Authorize]
        public async Task<IActionResult> CreateRent([FromBody] CreateRentDto createRentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (createRentDto.PickUpDate >= createRentDto.ReturnDate)            //Verifica que la fecha de devolucion sea antes que la de recoger el vehiculo
            {                               //Compara que el dia sea mayor, para que no contrate solo por horas
                return BadRequest("Invalid date: Return date cannot be before pick-up date.");
            }
            /*
            else if (createRentDto.PickUpDate == createRentDto.ReturnDate)
            {
                if (createRentDto.PickUpTime > createRentDto.ReturnTime)            //Verifica que la fecha de devolucion sea antes que la de recoger el vehiculo
                    return BadRequest("Invalid time: Pick-up time cannot be later than return time.");
                else if (createRentDto.PickUpTime == createRentDto.ReturnTime)
                    return BadRequest("Invalid time: Pick-up time cannot be later than return time.");
            }*/

            var rentDto = new RentDto
            {
                PickUpDate = createRentDto.PickUpDate,
                ReturnDate = createRentDto.ReturnDate,
                PickUpTime = createRentDto.PickUpTime,
                ReturnTime = createRentDto.ReturnTime,
                State = "pending",                          //Como es una nueva renta se carga como pendiente automaticamente
                Vehicle = createRentDto.Vehicle,

                VehicleId = createRentDto.VehicleId
            };

            try
            {
                var succeded = await _rentService.PlaceRentAsync(rentDto);
                if (succeded) return NoContent();
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
