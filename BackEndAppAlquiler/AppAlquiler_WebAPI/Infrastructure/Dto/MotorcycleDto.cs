using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class MotorcycleDto : VehicleDto
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int cilindrada { get; set; }//cilindrada(en cc)

        //RelationShips
        public int TypeId { get; set; }
    }
}
