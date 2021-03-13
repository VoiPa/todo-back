using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ToDoList.API.Services.Implementations;
using ToDoList.API.Services.Interfaces;
using ToDoList.DAL;
using ToDoList.DAL.Services.Implementations;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });
            
            /* SWAGGER */
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "TO-DO List",
                        Version = "v1"
                    });
            });
            return services;
        }
        
    }
}