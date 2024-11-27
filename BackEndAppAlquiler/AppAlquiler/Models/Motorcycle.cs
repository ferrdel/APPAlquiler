using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Motorcycle : Vehicle
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int Cilindrada { get; set; }//cilindrada(en cc)

        //Tipo_motocicleta(Cruiser, sport, touring, naked, dual-sport, ENUM.),
        
        //RelationShips
        public int TypeMotorcycleId { get; set; }
        public TypeMotorcycle TypeMotorcycle { get; set; }
    }
}
