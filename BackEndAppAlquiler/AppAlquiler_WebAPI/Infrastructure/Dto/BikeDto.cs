using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class BikeDto : VehicleDto
    {
        public int Whell { get; set; }// Rodado,
        public int FrameSize { get; set; }// tamaño_cuadro,
        public int NumberSpeeds { get; set; } // num_velocidades.(todo enum)
    }
}
