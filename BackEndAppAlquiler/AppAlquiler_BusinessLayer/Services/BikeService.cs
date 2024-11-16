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
    public class BikeService: IBikeService
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeService(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public async Task<IEnumerable<Bike>> GetAllBikeAsync()
        {
            var allBikes = await _bikeRepository.GetAllAsync();
            foreach (var bike in allBikes)                                //Cargamos el objeto model en todos los objetos de la clase
            {
                bike.Model = await _bikeRepository.GetModelByIdAsync(bike.ModelId);
            }
            return allBikes.ToList();
        }

        public async Task<Bike> GetBikeAsync(int id)
        {
            var succeeded = await _bikeRepository.GetByIdAsync(id);
            return succeeded;
        }

        public async Task<bool> AddBikeAsync(Bike bike)
        {
            try
            {
                await _bikeRepository.AddAsync(bike);
                await _bikeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //Aca captura el error
                return false;
            }
        }

        public async Task<bool> UpdateBikeAsync(Bike bike)
        {
            try
            {
                await _bikeRepository.UpdateAsync(bike);
                await _bikeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //Si modelo Id es incorrecto salta el error
                return false;
            }
        }

        public async Task<bool> DeleteBikeAsync(int id)
        {
            try
            {
                await _bikeRepository.DeleteAsync(await _bikeRepository.GetByIdAsync(id));
                await _bikeRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _bikeRepository.GetModelByIdAsync(id);
        }

    }
}
