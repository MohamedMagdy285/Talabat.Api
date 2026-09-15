using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tala.Core.Entities.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Kim Dokja",
                    Email = "KimDokja@gmail.com",
                    UserName = "KimDokja",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            } 

        }
    }
}
