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

        public Task<IEnumerable<Motorcycle>> GetAllTypeMotorcycle()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Motorcycle>> SearchTypeMotorcycle(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<Motorcycle> GetTypeMotorcycleByIdAsync(int id)
        {
            return await _context.Set<Motorcycle>().FindAsync(id);
        }

        public async Task<Motorcycle> GetBrandByIdAsync(int id)
        {
            return await _context.Set<Motorcycle>().FindAsync(id);
        }

        public async Task<Motorcycle> GetModelByIdAsync(int id)
        {
            return await _context.Set<Motorcycle>().FindAsync(id);
        }
    }
}
