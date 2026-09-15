using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tala.Core.Entities.Identity;
using Tala.Repository;
using Tala.Repository.Identity;

namespace Talab.Api.Extensions
{
    public static class SeederHandler
    {
        public static async Task<WebApplication> Seeder(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory= services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();
                await StoreContextSeeder.SeedAsync(dbContext);

                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync(); 

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, " An Error Occured During Applying Migrations !");
            }
            return app;
        }
    }
}
