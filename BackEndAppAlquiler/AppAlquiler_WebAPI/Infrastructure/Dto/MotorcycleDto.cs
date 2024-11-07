namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class MotorcycleDto : VehicleDto
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int Cilindrada { get; set; }//cilindrada(en cc)

        //RelationShips
        public int TypeMotorcycleId { get; set; }
    }
}
