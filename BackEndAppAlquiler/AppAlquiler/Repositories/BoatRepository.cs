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
    public class BoatRepository: Repository<Boat>, IBoatRepository
    {
        public BoatRepository(AlquilerDbContext context) : base(context) { }

        public async Task<Boat> GetBrandByIdAsync(int id)
        {
            return await _context.Set<Boat>().FindAsync(id);
        }

        public async Task<Boat> GetModelByIdAsync(int id)
        {
            return await _context.Set<Boat>().FindAsync(id);
        }
    }
}
