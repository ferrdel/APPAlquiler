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

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            var estado = await _context.Set<Brand>().FindAsync(id);     //Devuelve null si no lo encuentra
            return estado;
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _context.Set<Model>().FindAsync(id);
        }
    }
}
