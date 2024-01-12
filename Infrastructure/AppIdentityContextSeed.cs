using Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Ahmed",
                    Email = "ahmed@gmail.com",
                    UserName = "ahmedkhaled",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Khaled",
                        Street = "77",
                        State = "Cairo",
                        City = "Maadi",
                        ZipCode = "90120"
                    }
                };

                await userManager.CreateAsync(user,"Password123!");
            }
        }

    }
}
