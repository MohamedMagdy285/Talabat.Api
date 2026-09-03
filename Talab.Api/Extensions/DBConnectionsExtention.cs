using Microsoft.EntityFrameworkCore;
using Tala.Repository;

namespace Talab.Api.Extensions
{
    public static class DBConnectionsExtention
    {
        public static IServiceCollection SqlServerConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
