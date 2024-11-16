using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Repositories
{
    public class MotorcycleRepository : Repository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(AlquilerDbContext context) : base(context) { }

        public async Task<TypeMotorcycle> GetTypeMotorcycleByIdAsync(int id)
        {
            return await _context.Set<TypeMotorcycle>().FindAsync(id);
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            var model = await _context.Set<Model>().FindAsync(id);      //Aca brand viene nulo
            model.Brand = await _context.Set<Brand>().FindAsync(model.BrandId);     //Aca cargamos el valor de brand en model
            return model;
        }
    }
}
