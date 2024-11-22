using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
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
            var model = await _context.Set<Model>().FindAsync(id);      
            model.Brand = await _context.Set<Brand>().FindAsync(model.BrandId);     
            return model;
        }

        public async Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
        {
            return await _context.Motorcycles
                .Include(m => m.Model)
                    .ThenInclude(m => m.Brand)
                .Include(m => m.TypeMotorcycle)
                .ToListAsync();
        }

        public async Task<Motorcycle> GetMotorcycleByIdAsync(int id)
        {
            return await _context.Motorcycles
                .Include(c => c.Model)
                .Include(c => c.TypeMotorcycle)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
