using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Services
{
    public class RentService : IRentService
    {
        private readonly AlquilerDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RentService(AlquilerDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<RentDto>> GetAllRentAsync()
        {
            var rents = await _context.Rents
                .Select(r => new RentDto
                {
                    Id = r.Id,
                    PickUpDate = r.PickUpDate,
                    ReturnDate = r.ReturnDate,
                    PickUpTime = r.PickUpTime,
                    ReturnTime = r.ReturnTime,
                    State = r.State.ToString(),
                    Vehicle = r.Vehicle.ToString(),

                    UserDNI = r.User.DNI,
                    UserFirstName = r.User.FirstName,
                    UserLastName = r.User.LastName,
                    UserAddress = r.User.Address,
                    UserEmail = r.User.Email,
                    UserPhoneNumber = r.User.PhoneNumber,
                    UserCity = r.User.City,
                    UserCountry = r.User.Country,

                    UserId = r.User.Id,
                    VehicleId = r.VehicleId
                })
                .ToListAsync();

            return rents;
        }

        public async Task<IEnumerable<RentDto>> GetRentsByUserIdAsync(int id)
        {

            var rents = await _context.Rents.Where(o => o.UserId == id)
                .Select(r => new RentDto
                {
                    Id = r.Id,
                    PickUpDate = r.PickUpDate,
                    ReturnDate = r.ReturnDate,
                    PickUpTime = r.PickUpTime,
                    ReturnTime = r.ReturnTime,
                    State = r.State.ToString(),
                    Vehicle = r.Vehicle.ToString(),

                    UserDNI = r.User.DNI,
                    UserFirstName = r.User.FirstName,
                    UserLastName = r.User.LastName,
                    UserAddress = r.User.Address,
                    UserEmail = r.User.Email,
                    UserPhoneNumber = r.User.PhoneNumber,
                    UserCity = r.User.City,
                    UserCountry = r.User.Country,

                    VehicleId = r.VehicleId
                })
                .ToListAsync();

            return rents;
        }

        public async Task<RentDto> GetRentDtoByIdAsync(int id)
        {
            var rent = await _context.Rents.Include(c => c.User)
                .Select(r => new RentDto
                {
                    Id = r.Id,
                    PickUpDate = r.PickUpDate,
                    ReturnDate = r.ReturnDate,
                    PickUpTime = r.PickUpTime,
                    ReturnTime = r.ReturnTime,
                    State = r.State.ToString(),
                    Vehicle = r.Vehicle.ToString(),

                    UserDNI = r.User.DNI,
                    UserFirstName = r.User.FirstName,
                    UserLastName = r.User.LastName,
                    UserAddress = r.User.Address,
                    UserEmail = r.User.Email,
                    UserPhoneNumber = r.User.PhoneNumber,
                    UserCity = r.User.City,
                    UserCountry = r.User.Country,

                    UserId = r.User.Id,
                    VehicleId = r.VehicleId
                })
                .FirstOrDefaultAsync(c => c.Id == id);
            return rent;
        }

        public async Task<IEnumerable<MyRentDto>> GetMyRentsAsync()
        {
            var userId = (int)_currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var rents = await _context.Rents.Where(o => o.UserId == userId)
                .Select(r => new MyRentDto
                {
                    Id = r.Id,
                    PickUpDate = r.PickUpDate,
                    ReturnDate = r.ReturnDate,
                    PickUpTime = r.PickUpTime,
                    ReturnTime = r.ReturnTime,
                    State = r.State.ToString(),
                    Vehicle = r.Vehicle.ToString(),
                    VehicleId = r.VehicleId,
                })
                .ToListAsync();

            return rents;
        }

        public async Task<Rent> GetByIdAsync(int id)
        {
            var rent = await _context.Rents.FindAsync(id);
            return rent;
        }

        public async Task<bool> UpdateRentAsync(Rent rent)
        {
            try
            {
                _context.Rents.Update(rent);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PlaceRentAsync(RentDto rentDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                float price = GetVehicleByIdAsync(rentDto.Vehicle, rentDto.VehicleId);
                var userId = (int)_currentUserService.UserId;
                if (userId == null)
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }
                var rent = new Rent
                {

                    PickUpDate = rentDto.PickUpDate,
                    ReturnDate = rentDto.ReturnDate,
                    PickUpTime = rentDto.PickUpTime,
                    ReturnTime = rentDto.ReturnTime,
                    State = Enum.Parse<RentState>(rentDto.State),          //Se puede definir automaticamente como Pendiente
                    Vehicle = Enum.Parse<TypeVehicle>(rentDto.Vehicle),
                    TotAmount = CalculateDifferenceDays(rentDto.ReturnDate, rentDto.PickUpDate) * price,

                    VehicleId = rentDto.VehicleId,
                    UserId = userId
                };

                await _context.Rents.AddAsync(rent);
                await _context.SaveChangesAsync();


                await UpdateStateVehicleAsync(rentDto.Vehicle, rentDto.VehicleId);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        private float GetVehicleByIdAsync(string vehicle, int id)
        {
            float price = 0;
            switch (vehicle.ToLower())
            {
                case "bike":
                    var bike = _context.Bikes.FindAsync(id).Result; // Encuentra la bicicleta por ID
                    if (bike != null) price = bike.Price;
                    else throw new Exception("Bike not found");
                    break;
                case "boat":
                    var boat = _context.Boats.FindAsync(id).Result; // Encuentra el barco por ID
                    if (boat != null) price = boat.Price;
                    else throw new Exception("Boat not found");
                    break;
                case "motorcycle":
                    var motorcycle = _context.Motorcycles.FindAsync(id).Result; // Encuentra la motocicleta por ID
                    if (motorcycle != null) price = motorcycle.Price;
                    else throw new Exception("Motorcycle not found");
                    break;
                case "car":
                    var car = _context.Cars.FindAsync(id).Result; // Encuentra el auto por ID
                    if (car != null) price = car.Price;
                    else throw new Exception("Car not found");
                    break;
                default:
                    throw new ArgumentException("Vehicle type not recognized");
            }

            return price;
        }

        private float CalculateDifferenceDays(DateOnly pickUpDate, DateOnly returnDate)
        {
            int difference = (pickUpDate.DayNumber - returnDate.DayNumber);     //Devuelve la diferencia de dias
            return difference;
        }

        private async Task UpdateStateVehicleAsync(string vehicle, int id)
        {
            switch (vehicle.ToLower())
            {
                case "bike":
                    var bike = _context.Bikes.FindAsync(id).Result; // Encuentra la bicicleta por ID
                    if (bike != null)
                    {
                        if (bike.State == State.alquilado) throw new Exception("Bike is already rented");
                        bike.State = State.alquilado;
                        _context.Bikes.Update(bike);
                    }
                    else throw new Exception("Bike not found");
                    break;
                case "boat":
                    var boat = _context.Boats.FindAsync(id).Result; // Encuentra el barco por ID
                    if (boat != null)
                    {
                        if (boat.State == State.alquilado) throw new Exception("Boat is already rented");
                        boat.State = State.alquilado;
                        _context.Boats.Update(boat);
                    }
                    else throw new Exception("Boat not found");
                    break;
                case "motorcycle":
                    var motorcycle = _context.Motorcycles.FindAsync(id).Result; // Encuentra la motocicleta por ID
                    if (motorcycle != null)
                    {
                        if (motorcycle.State == State.alquilado) throw new Exception("Motorcycle is already rented");
                        motorcycle.State = State.alquilado;
                        _context.Motorcycles.Update(motorcycle);
                    }
                    else throw new Exception("Motorcycle not found");
                    break;
                case "car":
                    var car = _context.Cars.FindAsync(id).Result; // Encuentra el auto por ID
                    if (car != null)
                    {
                        if (car.State == State.alquilado) throw new Exception("Car is already rented");
                        car.State = State.alquilado;
                        _context.Cars.Update(car);
                    }
                    else throw new Exception("Car not found");
                    break;
                default:
                    throw new ArgumentException("Vehicle type not recognized");
            }
        }
    }
}
