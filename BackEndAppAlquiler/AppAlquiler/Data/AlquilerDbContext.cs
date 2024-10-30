using AppAlquiler_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Data
{
    public class AlquilerDbContext: DbContext
    {
        //constructor
        public AlquilerDbContext(DbContextOptions<AlquilerDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehiculos { get;set; }
        public DbSet<Car> Cars { get; set; } //autos
        public DbSet<Motorcycle> Motorcycles { get; set; } //motos
        public DbSet<Bike> Bikes { get; set; } //bicicletas
        public DbSet<Boat> Boats { get; set; } //Lanchas
        public DbSet<Brand> Brands { get;set; } //marcas
        public DbSet<Model> Models { get;set; }//modelos
        public DbSet<TypeMotorcycle> TypeMotorcycles { get; set; }//tipos de motocicletas
    }
}
