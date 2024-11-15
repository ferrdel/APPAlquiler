using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            //trae los datos que se enviaron, por ende nunca viene null
            return await _boatRepository.GetByIdAsync(id);    //Devuelve un arreglo provisional y se rompe dado que no es null
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
                //await _boatRepository.UpdateAsync(boat);
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


        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _boatRepository.GetBrandByIdAsync(id);
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _boatRepository.GetModelByIdAsync(id);
        }

    }
}
