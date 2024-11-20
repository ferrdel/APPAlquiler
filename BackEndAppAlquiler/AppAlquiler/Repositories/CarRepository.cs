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
    public class CarRepository: Repository<Car>, ICarRepository
    {
        public CarRepository(AlquilerDbContext context) : base(context) { }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            var model = await _context.Set<Model>().FindAsync(id);      //Aca brand viene nulo
            model.Brand = await _context.Set<Brand>().FindAsync(model.BrandId);     //Aca cargamos el valor de brand en model
            return model;
        }

        //agregado
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars
                .Include(m => m.Model)
                    .ThenInclude(m => m.Brand)                
                .ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.Include(c => c.Model).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
