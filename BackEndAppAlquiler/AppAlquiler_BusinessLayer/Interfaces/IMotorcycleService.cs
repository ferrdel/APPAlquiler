using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<Motorcycle>> GetAllMotorcycleAsync();
        Task<Motorcycle> GetMotorcycleAsync(int id);
        Task<bool> AddMotorcycleAsync(Motorcycle motorcycle);
        Task<bool> UpdateMotorcycleAsync(Motorcycle motorcycle);
        Task<bool> DeleteMotorcycleAsync(int id);

        Task<TypeMotorcycle> GetTypeMotorcycleByIdAsync(int id);
        Task<Model> GetModelByIdAsync(int id);
    }
}
