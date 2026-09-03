using Microsoft.AspNetCore.DataProtection;
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

            return services;
        }
    }
}
