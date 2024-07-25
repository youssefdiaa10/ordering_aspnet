using Microsoft.AspNetCore.Identity;
using Ordering.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
         
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Amin",
                    Email = "ahmedamin41@gmail.com",
                    UserName = "ahmed.amin",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(user, "Pa$$W0rd");
            }
        }

    }
}
