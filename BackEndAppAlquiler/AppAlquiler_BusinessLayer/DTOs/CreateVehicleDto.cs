﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.DTOs
{
    public class CreateVehicleDto
    {
        public string Description { get; set; }
        public string GasolineConsumption { get; set; }//cantidad de litros de nafta
        public string LuggageCapacity { get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string? Fuel { get; set; } //tipo combustible (ver bien el tipo de dato)
        public State State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public bool Active { get; set; } //Baja logica
        public float Price { get; set; }
        //public string TypeVehicle { get; set; }

        //RelationShips
        public int ModelId { get; set; }
        //definicion de la marca
        public int BrandId { get; set; }
    }
}
