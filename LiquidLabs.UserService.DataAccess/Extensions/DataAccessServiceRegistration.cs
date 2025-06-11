using LiquidLabs.UserService.DataAccess.Persistence;
using LiquidLabs.UserService.DataAccess.Repositories;
using LiquidLabs.UserService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidLabs.UserService.DataAccess.Extensions
{
    /// <summary>
    /// Extension methods to register data access related services into the dependency injection container.
    /// </summary>
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }
    }
}
