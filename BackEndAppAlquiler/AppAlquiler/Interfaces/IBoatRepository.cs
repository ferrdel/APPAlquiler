﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface IBoatRepository: IRepository<Boat>
    {
        Task<Boat> GetBrandByIdAsync(int id);
        Task<Boat> GetModelByIdAsync(int id);
    }
}