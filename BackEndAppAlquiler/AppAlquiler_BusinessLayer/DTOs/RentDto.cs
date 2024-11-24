﻿using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.DTOs
{
    public class RentDto
    {
        public int Id { get; set; }
        public DateOnly PickUpDate { get; set; } //fecha retiro         Ej: AAAA-MM-DD
        public DateOnly ReturnDate { get; set; } //fecha devolucionre
        public TimeOnly PickUpTime { get; set; } // hra retiro          Ej: HH:MM:SS
        public TimeOnly ReturnTime { get; set; } //hra devolucion   
        private string _state { get; set; }     // pending, confirmed, rejected
        public string State
        {
            get { return _state; }
            set { _state = value.ToLower(); }           //Pasamos a minusculas todas las letras, para evitar errores por una letra con mayuscula
        }
        private string _vehicle { get; set; }       //bike, boat, car, motorcycle
        public string Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value.ToLower(); }         //Pasamos a minusculas todas las letras, para evitar errores por una letra con mayuscula
        }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserDNI { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }
        public string UserRegion { get; set; }

        //RelationShips
        public int VehicleId { get; set; }
        public int UserId { get; set; }
    }
}