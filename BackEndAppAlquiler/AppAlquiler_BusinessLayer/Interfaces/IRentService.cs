using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface IRentService
    {
        Task<IEnumerable<RentDto>> GetAllRentAsync();
        Task<IEnumerable<RentDto>> GetMyRentsAsync();
        Task<IEnumerable<RentDto>> GetRentsByUserIdAsync(int id);
        Task<RentDto> GetByIdAsync(int id);

        Task<bool> UpdateRentAsync(RentDto rentDto);
        Task<bool> PlaceRentAsync(RentDto rentDto);

    }
}
