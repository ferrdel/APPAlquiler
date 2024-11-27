using AppAlquiler_DataAccessLayer.Models;
using Mono.TextTemplating;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class CreateRentDto
    {
        public int? Id { get; set; }
        public DateOnly PickUpDate { get; set; } //fecha retiro
        public DateOnly ReturnDate { get; set; } //fecha devolucionre
        public TimeOnly PickUpTime { get; set; } // hra retiro
        public TimeOnly ReturnTime { get; set; } //hra devolucion
        private string _vehicle { get; set; }       //bike, boat, car, motorcycle
        public string Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value.ToLower(); }         //Pasamos a minusculas todas las letras, para evitar errores por una letra con mayuscula
        }

        //RelationShips
        public int VehicleId { get; set; }
    }
}
