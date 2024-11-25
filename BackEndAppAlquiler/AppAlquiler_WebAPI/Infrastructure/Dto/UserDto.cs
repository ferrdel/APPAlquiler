using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DNI { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Generate { get; set; }
        public bool Active { get; set; }
        public string Token {  get; set; }

        //relationship
       // public int ProfileId { get; set; }
    }
}
