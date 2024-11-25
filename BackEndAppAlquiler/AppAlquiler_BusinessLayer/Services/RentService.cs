using AppAlquiler_BusinessLayer.DTOs;
using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
                    UserRegion = r.User.Region,

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
                    UserRegion = r.User.Region,

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
                    UserRegion = r.User.Region,

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
            var userName = _currentUserService.UserName;

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

        public async Task<bool> PlaceRentAsync(Rent rent)
        {
            try
            {
                await _context.Rents.AddAsync(rent);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
