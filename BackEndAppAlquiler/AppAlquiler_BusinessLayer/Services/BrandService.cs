using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_DataAccessLayer.Data;
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
            return true;
        }
    }
}
