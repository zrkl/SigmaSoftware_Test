using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;


namespace Persistence
{
    /// <summary>
    /// Class to manage the injections
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Method to add sql server persistence 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //Adding DbContext for MSSqlServer
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}