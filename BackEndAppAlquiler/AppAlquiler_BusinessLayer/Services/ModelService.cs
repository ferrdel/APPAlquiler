using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Services
{
    public class ModelService
    {
        private readonly AlquilerDbContext _context;

        public ModelService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PlaceBrand(CreateModelDto brandDto)
        {
            return true;
        }
    }
}
