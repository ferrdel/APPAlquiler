namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class VehicleDetailsDto
    {
        public int? Id { get; set; }     //Agregado para front
        public string Description { get; set; }
        public float GasolineConsumption { get; set; }//cantidad de litros de nafta
        public float LuggageCapacity { get; set; }//capacidad de equipaje(En Litros)
        public int PassengerCapacity { get; set; } //Cantiadad de pasajeros
        //definicion combustible
        public string? Fuel { get; set; } //tipo combustible (ver bien el tipo de dato)

        //[EnumDataType(typeof(State), ErrorMessage = "{0} Indicado no es valido")]
        private string _state { get; set; } //Alquilado,Disponible,EnMantenimiento
        public string State
        {
            get { return _state; }
            set { _state = value.ToLower(); }           //Pasamos a minusculas todas las letras, para evitar errores por una letra con mayuscula
        }
        public bool Active { get; set; } //Baja logica
        public float Price { get; set; }
        public string? Image { get; set; }

        //RelationShips
        public string Model { get; set; }
        //definicion de la marca
        public string Brand { get; set; }
    }
}
