using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface ITypeMotorcycleService
    {
        Task<IEnumerable<TypeMotorcycle>> GetAllTypeMotorcycleAsync();
        Task<TypeMotorcycle> GetTypeMotorcycleAsync(int id);
        Task<bool> AddTypeMotorcycleAsync(TypeMotorcycle typeMotorcycle);
        Task<bool> UpdateTypeMotorcycleAsync(TypeMotorcycle typeMotorcycle);
        Task<bool> DeleteTypeMotorcycleAsync(int id);
    }
}
