namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class MotorcycleDetailsDto : VehicleDetailsDto
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int Cilindrada { get; set; }//cilindrada(en cc)

        //RelationShips
        public string TypeMotorcycle { get; set; }
    }
}
