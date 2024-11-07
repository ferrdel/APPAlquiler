using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface IMotorcycleRepository: IRepository<Motorcycle>
    {
        Task<IEnumerable<Motorcycle>> GetAllTypeMotorcycle();
        Task<IEnumerable<Motorcycle>> SearchTypeMotorcycle(string searchTerm);

        Task<Motorcycle> GetTypeMotorcycleByIdAsync(int id);

        Task<Motorcycle> GetBrandByIdAsync(int id);
        Task<Motorcycle> GetModelByIdAsync(int id);
    }
}
