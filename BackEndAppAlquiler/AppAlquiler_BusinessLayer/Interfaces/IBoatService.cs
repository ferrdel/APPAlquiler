using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface IBoatService
    {
        Task<IEnumerable<Boat>> GetAllBoatAsync();
        Task<Boat> GetBoatAsync(int id);
        Task<bool> AddBoatAsync(Boat boat);
        Task<bool> UpdateBoatAsync(Boat boat);
        Task<bool> DeleteBoatAsync(int id);
        Task<bool> ActivateAsync(int id);

        Task<Model> GetModelByIdAsync(int id);
    }
}
