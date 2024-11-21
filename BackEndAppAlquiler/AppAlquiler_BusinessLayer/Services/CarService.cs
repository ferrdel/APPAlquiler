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
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetAllCarAsync()
        {
            var allCars = await _carRepository.GetAllCarsAsync();
            return allCars;
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _carRepository.GetCarByIdAsync(id);
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            try
            {
                await _carRepository.AddAsync(car);
                await _carRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            try
            {
                await _carRepository.UpdateAsync(car);
                await _carRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            try
            {
                await _carRepository.DeleteAsync(await _carRepository.GetByIdAsync(id));
                await _carRepository.SaveChangesAsync();
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
                await _carRepository.ActivateAsync(await _carRepository.GetByIdAsync(id));
                await _carRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _carRepository.GetModelByIdAsync(id);
        }
    }
}
