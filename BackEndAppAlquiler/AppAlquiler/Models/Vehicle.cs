using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    public class Vehicle
    {
        //(codVehiculo, descripcion, marca, modelo, consumo, combustible(nafta, gasoil, eléctrico, ninguno),
        //capacidad de equipaje(En Litros), capacidad de pasajeros, disponible(boolean), precioPorDia
        public int Id { get; set; }
        public string Description { get; set; }
        public string GasolineConsumption { get; set; }//cantidad de litros de nafta
        public string LuggageCapacity {  get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public int? Fuel { get; set; } //tipo combustible
        public string State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public  float Price {  get; set; }

        //RelationShips

        public int ModelID { get; set; }
        public Model Model { get; set; }
        //definicion de la marca
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
