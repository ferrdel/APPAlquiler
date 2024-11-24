using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{    
    public enum RentState
    {
        pending,
        confirmed,
        rejected
    }

    public enum TypeVehicle
    {
        bike,
        boat,
        car,
        motorcycle
    }

    public class Rent
    {
        public int Id { get; set; }
        public DateOnly PickUpDate { get; set; } //fecha retiro
        public DateOnly ReturnDate { get; set; } //fecha devolucionre
        public TimeOnly PickUpTime { get; set; } // hra retiro
        public TimeOnly ReturnTime { get; set; } //hra devolucion
        public RentState State { get; set; } // Pendiente, Confirmado, Rechazado

        public TypeVehicle Vehicle { get; set; }

        //RelationShips
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
