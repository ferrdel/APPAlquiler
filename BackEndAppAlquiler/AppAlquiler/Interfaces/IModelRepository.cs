﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface IModelRepository: IRepository<Model>
    {
        Task<Brand> GetBrandByIdAsync(int id);

        Task<IEnumerable<Model>> GetAllModelsAsync();
        //Task<Model> GetModelByIdAsync(int id);
    }
}
