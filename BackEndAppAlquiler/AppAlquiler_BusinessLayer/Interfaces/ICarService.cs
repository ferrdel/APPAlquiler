using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarAsync();
        Task<Car> GetCarAsync(int id);
        Task<bool> AddCarAsync(Car car);
        Task<bool> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int id);
        Task<bool> ActivateAsync(int id);

        Task<Model> GetModelByIdAsync(int id);
    }
}
