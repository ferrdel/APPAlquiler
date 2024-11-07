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
    public class BoatService: IBoatService
    {
        private readonly IBoatRepository _boatRepository;

        public BoatService(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public async Task<IEnumerable<Boat>> GetAllBoatAsync()
        {
            return await _boatRepository.GetAllAsync();
        }

        public async Task<Boat> GetBoatAsync(int id)
        {
            return await _boatRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddBoatAsync(Boat boat)
        {
            try
            {
                await _boatRepository.AddAsync(boat);
                await _boatRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBoatAsync(Boat boat)
        {
            try
            {
                await _boatRepository.UpdateAsync(boat);
                await _boatRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBoatAsync(int id)
        {
            try
            {
                await _boatRepository.DeleteAsync(await _boatRepository.GetByIdAsync(id));
                await _boatRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<Boat> GetBrandByIdAsync(int id)
        {
            return await _boatRepository.GetBrandByIdAsync(id);
        }

        public async Task<Boat> GetModelByIdAsync(int id)
        {
            return await _boatRepository.GetModelByIdAsync(id);
        }

    }
}
