using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface IBikeRepository: IRepository<Bike>
    {
        Task<Model> GetModelByIdAsync(int id);

        Task<IEnumerable<Bike>> GetAllBikesAsync();
        Task<Bike> GetBikeByIdAsync(int id);
    }
}
