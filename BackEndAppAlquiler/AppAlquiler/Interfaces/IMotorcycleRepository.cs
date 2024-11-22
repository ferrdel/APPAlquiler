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
        Task<TypeMotorcycle> GetTypeMotorcycleByIdAsync(int id);
        Task<Model> GetModelByIdAsync(int id);

        Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
        Task<Motorcycle> GetMotorcycleByIdAsync(int id);
    }
}
