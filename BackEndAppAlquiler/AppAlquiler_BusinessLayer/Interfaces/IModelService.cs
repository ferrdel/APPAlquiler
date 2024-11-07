using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetAllModelAsync();
        Task<Model> GetModelAsync(int id);
        Task<bool> AddModelAsync(Model model);
        Task<bool> UpdateModelAsync(Model model);
        Task<bool> DeleteModelAsync(int id);
    }
}
