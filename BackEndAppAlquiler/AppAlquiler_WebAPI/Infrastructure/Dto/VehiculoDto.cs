using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class VehiculoDto
    {
        public string Description { get; set; }
        public string GasolineConsumption { get; set; }//cantidad de litros de nafta
        public int LuggageCapacity { get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string Fuel { get; set; } //tipo combustible (ver bien el tipo de dato)
        public string State { get; set; } //Alquilado,Disponible,EnMantenimiento
        public float Price { get; set; }

        //RelationShips
        public Model IdModel { get; set; }
        //definicion de la marca
        public Brand IdBrand { get; set; }
    }
}
