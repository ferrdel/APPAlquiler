using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Data;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

        public async Task<Brand> GetBrandAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddBrandAsync(Brand brand)
        {
            try
            {
                await _brandRepository.AddAsync(brand);
                await _brandRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBrandAsync(Brand brand)
        {
            try
            {
                await _brandRepository.UpdateAsync(brand);
                await _brandRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            try
            {
                await _brandRepository.DeleteAsync(await _brandRepository.GetByIdAsync(id));
                await _brandRepository.SaveChangesAsync();
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
                await _brandRepository.ActivateAsync(await _brandRepository.GetByIdAsync(id));
                await _brandRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
