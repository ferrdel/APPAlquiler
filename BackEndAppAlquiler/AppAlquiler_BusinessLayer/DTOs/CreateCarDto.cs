using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.DTOs
{
    public class CreateCarDto : CreateVehicleDto
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
