namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class BikeDetailsDto : VehicleDetailsDto
    {
        public int Whell { get; set; }// Rodado,
        public int FrameSize { get; set; }// tamaño_cuadro,
        public int NumberSpeeds { get; set; } // num_velocidades.(todo enum)
    }
}
