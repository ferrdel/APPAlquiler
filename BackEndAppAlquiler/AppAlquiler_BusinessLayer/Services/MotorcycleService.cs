using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_DataAccessLayer.Repositories;
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
            var allMotorcycles = await _motorcycleRepository.GetAllMotorcyclesAsync();
            return allMotorcycles;
        }

        public async Task<Motorcycle> GetMotorcycleAsync(int id)
        {
            return await _motorcycleRepository.GetMotorcycleByIdAsync(id);
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
        public async Task<bool> ActivateAsync(int id)
        {
            try
            {
                await _motorcycleRepository.ActivateAsync(await _motorcycleRepository.GetByIdAsync(id));
                await _motorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TypeMotorcycle> GetTypeMotorcycleByIdAsync(int id)
        {
            return await _motorcycleRepository.GetTypeMotorcycleByIdAsync(id);
        }


        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _motorcycleRepository.GetModelByIdAsync(id);
        }

        
    }
}
