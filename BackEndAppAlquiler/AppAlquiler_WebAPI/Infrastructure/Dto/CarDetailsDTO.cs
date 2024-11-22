namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class CarDetailsDto : VehicleDetailsDto
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
