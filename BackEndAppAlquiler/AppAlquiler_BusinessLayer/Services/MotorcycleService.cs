using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        
        public MotorcycleService(IMotorcycleRepository motorcycleRepository) 
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<IEnumerable<Motorcycle>> GetAllMotorcycleAsync()
        {
            return await _motorcycleRepository.GetAllAsync();
        }

        public async Task<Motorcycle> GetMotorcycleAsync(int id)
        {
            return await _motorcycleRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddMotorcycleAsync(Motorcycle motorcycle)
        {
            try
            {
                await _motorcycleRepository.AddAsync(motorcycle);
                await _motorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMotorcycleAsync(Motorcycle motorcycle)
        {
            try
            {
                await _motorcycleRepository.UpdateAsync(motorcycle);
                await _motorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMotorcycleAsync(int id)
        {
            try
            {
                await _motorcycleRepository.DeleteAsync(await _motorcycleRepository.GetByIdAsync(id));
                await _motorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*public Task<IEnumerable<Motorcycle>> GetAllTypeMotorcycleAsync()
        {
            throw new NotImplementedException();
        }*/

        public async Task<Motorcycle> GetTypeMotorcycleByIdAsync(int id)
        {
            return await _motorcycleRepository.GetTypeMotorcycleByIdAsync(id);
        }

        public async Task<Motorcycle> GetBrandByIdAsync(int id)
        {
            return await _motorcycleRepository.GetBrandByIdAsync(id);
        }

        public async Task<Motorcycle> GetModelByIdAsync(int id)
        {
            return await _motorcycleRepository.GetModelByIdAsync(id);
        }
    }
}
