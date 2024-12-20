﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Interfaces
{
    public interface IBrandRepository: IRepository<Brand>
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
