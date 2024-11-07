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
        Task<Bike> GetBrandByIdAsync(int id);
        Task<Bike> GetModelByIdAsync(int id);
    }
}
