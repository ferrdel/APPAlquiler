using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Services
{
    public class BrandService
    {
        private readonly AlquilerDbContext _context;

        public BrandService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PlaceBrand(CreateBrandDto brandDto)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var brand = new Brand
                {
                    Name = brandDto.Name,
                    Active = brandDto.Active
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;           //Se puede encadenar con una exepcion customizada
            }
        }
    }
}
