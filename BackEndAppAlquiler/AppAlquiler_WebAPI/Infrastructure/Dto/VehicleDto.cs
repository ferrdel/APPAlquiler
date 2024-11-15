using AppAlquiler_DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class VehicleDto
    {
        public int? Id { get; set; }     //Agregado para front
        public string Description { get; set; }
        public float GasolineConsumption { get; set; }//cantidad de litros de nafta
        public float LuggageCapacity { get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string? Fuel { get; set; } //tipo combustible (ver bien el tipo de dato)

        [EnumDataType(typeof(State), ErrorMessage = "{0} Indicado no es valido")]
        public string State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public bool Active { get; set; } //Baja logica
        public float Price { get; set; }
        public string Image { get; set; }
        //public string TypeVehicle { get; set; }

        //RelationShips
        public int ModelId { get; set; }
        //definicion de la marca
        public int BrandId { get; set; }
    }
}
