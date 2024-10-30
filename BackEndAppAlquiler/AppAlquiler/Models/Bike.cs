using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Bike : Vehicle
    {
        public int Whell { get; set; }// Rodado,
        public int FrameSize { get; set; }// tamaño_cuadro,
        public int NumberSpeeds { get; set; } // num_velocidades.(todo enum)
    }
}
