using Infrastructure.Repository;
using Infrastructure.Repositroy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("defaultConnectionString")!));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

    }
}
