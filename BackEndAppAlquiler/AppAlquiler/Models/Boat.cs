using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Boat : Vehicle
    {
        public string Dimension { get; set; }//Tamaño,
        public string Engine { get; set; }//Motor(fuera de borda, intraborda. ENUM),
        public string Material { get; set; }//material_construcción (Fibra de vidrio, aluminio, madera, etc),
        public string Stability { get; set; }//estabilidad (monocasco, catamarán, etc.),
        public string Navigation { get; set; }//navegación: (GPS, sonda, radio VHF),
        public string Facilities { get; set; }//comodidades/instalaciones (Asientos, toldos, cocineta, baño),
        public string Sound { get; set; }//sistema de sonido (Altavoces y conectividad),
        public string Accessories { get; set; }//accesorios (Remolque, ancla, escaleras, etc),
        public string Propulsion { get; set; }//propulsión (Tipo de hélice y sistema de dirección).

    }
}
