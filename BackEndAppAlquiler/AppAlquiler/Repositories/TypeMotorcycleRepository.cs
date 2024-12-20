﻿using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Repositories
{
    public class TypeMotorcycleRepository: Repository<TypeMotorcycle>, ITypeMotorcycleRepository
    {
        public TypeMotorcycleRepository(AlquilerDbContext context) : base(context) { }
    }
}
