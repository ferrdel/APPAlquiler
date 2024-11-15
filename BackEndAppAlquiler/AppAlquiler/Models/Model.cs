using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } //Baja logica

        //Relationships
        //public int BrandId { get; set; }
        //public Brand Brand { get; set; }
    }
}
