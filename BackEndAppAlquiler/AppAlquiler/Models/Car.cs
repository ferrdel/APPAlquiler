using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Car : Vehiculo
    {
        public int NumberDoors { get; set; } //cantidad puertas
        public bool AirConditioning { get; set; } //Aire acondicionado
        public string Transmission { get; set; } //transmisión(manual, automático), 
        public bool Airbag { get; set; } // Airbag(boleano),
        public bool Abs { get; set; } // Abs(boleano),
        public string Sound { get; set; } //Sistema_sonido(Altavoces y conectividad),
        public float EngineLiters { get; set; } //litrosPorMotor(enum 1.4)
    }
}
