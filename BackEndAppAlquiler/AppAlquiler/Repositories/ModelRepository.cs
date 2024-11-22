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
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(AlquilerDbContext context) : base(context) { }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _context.Set<Brand>().FindAsync(id);
        }
        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _context.Models.Include(m => m.Brand).ToListAsync();
        }
    }
}
