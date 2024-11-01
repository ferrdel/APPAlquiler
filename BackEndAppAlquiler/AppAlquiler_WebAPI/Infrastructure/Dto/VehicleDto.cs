using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class VehicleDto
    {
        public string Description { get; set; }
        public string GasolineConsumption { get; set; }//cantidad de litros de nafta
        public string LuggageCapacity { get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string? Fuel { get; set; } //tipo combustible (ver bien el tipo de dato)
        public bool State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public float Price { get; set; }
        //public string TypeVehicle { get; set; }

        //RelationShips
        public int ModelId { get; set; }
        //definicion de la marca
        public int BrandId { get; set; }
    }
}
