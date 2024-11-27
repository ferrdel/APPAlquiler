using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{   
    public enum State
    {
        alquilado,
        disponible,
        enmantenimiento
    }
    public class Vehicle
    {
        //(codVehiculo, descripcion, marca, modelo, consumo, combustible(nafta, gasoil, eléctrico, ninguno),
        //capacidad de equipaje(En Litros), capacidad de pasajeros, disponible(boolean), precioPorDia
        public int Id { get; set; }
        public string Description { get; set; }
        public float GasolineConsumption { get; set; }//cantidad de litros de nafta
        public float LuggageCapacity {  get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string? Fuel { get; set; } //tipo combustible
        public State State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public bool Active { get; set; } //Baja logica
        public float Price { get; set; }
        public string? Image { get; set; }
        // public string TypeVehicle { get; set; } //defino tipo para poder instanciarlo en el controller (bike,boat,car,motorcycle)

        //RelationShips

        public int ModelId { get; set; }
        public Model Model { get; set; }
        //definicion de la marca
        //public int BrandId { get; set; }
        //public Brand Brand { get; set; }
    }
}
