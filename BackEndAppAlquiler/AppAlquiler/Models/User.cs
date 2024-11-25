using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Models
{
    

    public class User : IdentityUser<int>
    {
        //public int Id { get; set; }
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

        //relationship
        //public int ProfileId { get; set; }
        //public Profile Profile { get; set; }
        
    }
}
