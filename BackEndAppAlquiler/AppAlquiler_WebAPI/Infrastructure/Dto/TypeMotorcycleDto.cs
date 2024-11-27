using AppAlquiler_DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class TypeMotorcycleDto
    {
        public int? Id { get; set; }     //Agregado para front
        public string Name { get; set; }    
        public bool Active { get; set; } //Baja logica
    }
}
