using AppAlquiler_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Data
{
    public class AlquilerDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        //constructor
        public AlquilerDbContext(DbContextOptions<AlquilerDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; } //autos
        public DbSet<Motorcycle> Motorcycles { get; set; } //motos
        public DbSet<Bike> Bikes { get; set; } //bicicletas
        public DbSet<Boat> Boats { get; set; } //Lanchas
        public DbSet<Brand> Brands { get;set; } //marcas
        public DbSet<Model> Models { get;set; }//modelos
        public DbSet<TypeMotorcycle> TypeMotorcycles { get; set; }//tipos de motocicletas
        public DbSet<Rent> Rents { get; set; }

    }
}

