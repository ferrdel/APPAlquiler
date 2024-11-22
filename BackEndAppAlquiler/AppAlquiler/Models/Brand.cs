using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public bool Active { get; set; } //Baja logica

        // Relación de 1 a N (una Marca tiene muchos Modelos)
        public List<Model> Models { get; set; }  // Navegación hacia los Modelos

    }
}
