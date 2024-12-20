﻿using AppAlquiler_BusinessLayer.Interfaces;
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
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Model>> GetAllModelAsync()
        {
            var allModels = await _modelRepository.GetAllModelsAsync();
            return allModels;
        }

        public async Task<Model> GetModelAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }
        public async Task<bool> AddModelAsync(Model model)
        {
            try
            {
                await _modelRepository.AddAsync(model);
                await _modelRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateModelAsync(Model model)
        {
            try
            {
                await _modelRepository.UpdateAsync(model);
                await _modelRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteModelAsync(int id)
        {
            try
            {
                await _modelRepository.DeleteAsync(await _modelRepository.GetByIdAsync(id));
                await _modelRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _modelRepository.GetBrandByIdAsync(id);
        }

        public async Task<bool> ActivateAsync(int id)
        {
            try
            {
                await _modelRepository.ActivateAsync(await _modelRepository.GetByIdAsync(id));
                await _modelRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
