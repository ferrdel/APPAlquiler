using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public enum NameTypeMotorcycle
    {
        cruiser,
        sport,
        touring,
        naked, 
        dualsport
    }

    public class TypeMotorcycle
    {
        public int Id { get; set; }
        public NameTypeMotorcycle Name { get; set; }
        public bool Active { get; set; } //Baja logica
    }
}
