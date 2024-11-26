using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Seeds
{
    public class SeedUsersAndRoles
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AlquilerDbContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                //faltaba serviceScope.
                if (!context.Roles.Any())
                {
                    var adminRole = new IdentityRole<int>("ADMIN");
                    await roleManager.CreateAsync(adminRole);               

                    var userRole = new IdentityRole<int>("USER");             //Cambie de CLIENT a USER
                    await roleManager.CreateAsync(userRole);
                }

                if (!context.Users.Any())
                {
                    var user = new User
                    {
                        UserName = "admin",
                        Email = "admin@email.com",
                        FirstName = "admin",
                        LastName = "lastAdmin",
                        DNI = 01234567,
                        PhoneNumber = "123456789",
                        Address = "address",
                        City = "City",
                        Country = "Country"
                    };

                    var result = await userManager.CreateAsync(user, "adminadmin");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "ADMIN");
                    }
                    else
                    {
                        //Error
                    }

                }

            }
        }
    }
}
