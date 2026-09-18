using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Tala.Core.Entities.Identity;
using Tala.Core.Repositories;
using Tala.Core.Services;
using Tala.Repository;
using Tala.Repository.Identity;
using Tala.Services;
using Talab.Api.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talab.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //// Allow DependancyInjection for ProductController ONLY => Bad !
            //builder.Services.AddScoped<IGenericRepository<Product> , GenericRepository<Product>>();
            // hna b2olo y3ml Create l ay haga gaya mn IGeneric => Allow DependancyInjection = GOOD :D
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));

            services.AddScoped(typeof(ITokenService), typeof(TokenService));
 
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}
