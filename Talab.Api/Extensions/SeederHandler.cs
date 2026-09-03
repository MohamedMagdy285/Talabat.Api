using Microsoft.EntityFrameworkCore;
using Tala.Repository;

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
