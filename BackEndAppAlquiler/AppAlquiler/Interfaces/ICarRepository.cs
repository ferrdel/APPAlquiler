﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface ICarRepository: IRepository<Car>
    {
        Task<Car> GetBrandByIdAsync(int id);
        Task<Car> GetModelByIdAsync(int id);
    }
}