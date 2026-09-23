
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tala.Core.Entities.Identity;
using Tala.Core.Services;
using Tala.Repository.Identity;
using Tala.Services;
using Talab.Api.Extensions;

namespace Talab.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
            });
            var config = builder.Configuration;
            builder.Services.SqlServerConnection(config);
            builder.Services.RedisConnection(config);
            builder.Services.UserIdentityConnection(config);
            builder.Services.AddApplicationServices();

          
            builder.Services.AddJwtAuthentication(config);
            //builder.Services.AddAuthentication("Bearer"); 
            var app = builder.Build();

            app.Seeder();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();
             
            app.Run();
        }
    }
}
