using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface IBikeService
    {
        Task<IEnumerable<Bike>> GetAllBikeAsync();
        Task<Bike> GetBikeAsync(int id);
        Task<bool> AddBikeAsync(Bike bike);
        Task<bool> UpdateBikeAsync(Bike bike);
        Task<bool> DeleteBikeAsync(int id);

        Task<Brand> GetBrandByIdAsync(int id);
        Task<Model> GetModelByIdAsync(int id);
    }
}
