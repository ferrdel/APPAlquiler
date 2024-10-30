using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Motorcycle : Vehiculo
    {
        public bool Abs { get; set; }//Abs(boleano),
        public int cilindrada { get; set; }//cilindrada(en cc)

        //Tipo_motocicleta(Cruiser, sport, touring, naked, dual-sport, ENUM.),
        
        //RelationShips
        public TypeMotorcycle Id { get; set; }
        public TypeMotorcycle TypeMotorcycle { get; set; }
    }
}
