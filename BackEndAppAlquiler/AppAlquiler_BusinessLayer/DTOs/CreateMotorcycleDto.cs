using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.DTOs
{
    public class CreateMotorcycleDto : CreateVehicleDto
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int cilindrada { get; set; }//cilindrada(en cc)

        //RelationShips
        public int TypeId { get; set; }
    }
}
