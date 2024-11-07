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
    public class CarRepository: Repository<Car>, ICarRepository
    {
        public CarRepository(AlquilerDbContext context) : base(context) { }

        public async Task<Car> GetBrandByIdAsync(int id)
        {
            return await _context.Set<Car>().FindAsync(id);
        }

        public async Task<Car> GetModelByIdAsync(int id)
        {
            return await _context.Set<Car>().FindAsync(id);
        }
    }
}
