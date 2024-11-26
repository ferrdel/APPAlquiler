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
        Task<IEnumerable<RentDto>> GetRentsByUserIdAsync(int id);
        Task<RentDto> GetRentDtoByIdAsync(int id);
        Task<IEnumerable<MyRentDto>> GetMyRentsAsync();
        Task<Rent> GetByIdAsync(int id);            //Se utiliza para actualizar los datos y verificar la existencia
        Task<float> GetVehicleByIdAsync(string vehicle, int id);      //Se utiliza para buscar el precio del vehiculo

        Task<bool> UpdateRentAsync(Rent rent);
        Task<bool> PlaceRentAsync(Rent rent);

    }
}
