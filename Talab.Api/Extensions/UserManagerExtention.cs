using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tala.Core.Entities.Identity;

namespace Talab.Api.Extensions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser?> FindUserAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email.Value);
            return user;    
        }
    }
}
