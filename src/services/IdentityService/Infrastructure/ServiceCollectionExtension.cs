using Infrastructure.Repository;
using Infrastructure.Repositroy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

    }
}
