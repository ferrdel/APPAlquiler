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
    public class TypeMotorcycleService : ITypeMotorcycleService
    {
        private readonly ITypeMotorcycleRepository _typeMotorcycleRepository;

        public TypeMotorcycleService(ITypeMotorcycleRepository typeMotorcycleRepository)
        {
            _typeMotorcycleRepository = typeMotorcycleRepository;
        }

        public async Task<IEnumerable<TypeMotorcycle>> GetAllTypeMotorcycleAsync()
        {
            return await _typeMotorcycleRepository.GetAllAsync();
        }

        public async Task<TypeMotorcycle> GetTypeMotorcycleAsync(int id)
        {
            return await _typeMotorcycleRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddTypeMotorcycleAsync(TypeMotorcycle typeMotorcycle)
        {
            try
            {
                await _typeMotorcycleRepository.AddAsync(typeMotorcycle);
                await _typeMotorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTypeMotorcycleAsync(TypeMotorcycle typeMotorcycle)
        {
            try
            {
                await _typeMotorcycleRepository.UpdateAsync(typeMotorcycle);
                await _typeMotorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTypeMotorcycleAsync(int id)
        {
            try
            {
                await _typeMotorcycleRepository.DeleteAsync(await _typeMotorcycleRepository.GetByIdAsync(id));
                await _typeMotorcycleRepository.SaveChangesAsync();
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
                await _typeMotorcycleRepository.ActivateAsync(await _typeMotorcycleRepository.GetByIdAsync(id));
                await _typeMotorcycleRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
