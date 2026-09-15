using Microsoft.AspNetCore.DataProtection;
using Tala.Core.Repositories;
using Tala.Repository;
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

            return services;
        }
    }
}
