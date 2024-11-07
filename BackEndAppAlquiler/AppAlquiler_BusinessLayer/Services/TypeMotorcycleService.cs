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
    public class TypeMotorcycleService : ITypeMotorcycleService
    {
        private readonly ITypeMotorcycleRepository _repository;

        public TypeMotorcycleService(ITypeMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TypeMotorcycle>> GetAllTypeMotorcycleAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TypeMotorcycle> GetTypeMotorcycleAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddTypeMotorcycleAsync(TypeMotorcycle typeMotorcycle)
        {
            try
            {
                await _repository.AddAsync(typeMotorcycle);
                await _repository.SaveChangesAsync();
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
                await _repository.UpdateAsync(typeMotorcycle);
                await _repository.SaveChangesAsync();
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
                await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
