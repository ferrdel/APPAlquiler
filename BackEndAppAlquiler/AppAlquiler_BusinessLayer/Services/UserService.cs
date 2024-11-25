using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;

namespace AppAlquiler_BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar --> " + ex.Message);
                //Aca captura el error
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //Si modelo Id es incorrecto salta el error
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(await _userRepository.GetByIdAsync(id));
                await _userRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> GetProfileAsync(int id)
        {
            return await _userRepository.GetProfileIdAsync(id);
        }
    }
}
