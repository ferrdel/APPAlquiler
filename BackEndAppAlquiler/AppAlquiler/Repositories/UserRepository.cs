using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AlquilerDbContext context) : base(context) { }

        public async Task<User> GetProfileIdAsync(int Id)
        {
            return await _context.Set<User>().FindAsync(Id);
        }

    }
}
