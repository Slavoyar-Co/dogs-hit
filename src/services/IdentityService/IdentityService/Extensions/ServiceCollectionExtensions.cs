using IdentityService.Implementations;
using IdentityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityService.ServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationProviders(this IServiceCollection services, string key)
        {
            services.AddTransient<IJwtAuthentificationManager, JwtAuthentificationManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator
                };
            });


            //TODO add external providers

            return services;
        }

        private static LifetimeValidator LifetimeValidator = 
            (DateTime? notBefore, 
            DateTime? expires, 
            SecurityToken securityToken, 
            TokenValidationParameters validationParameters) =>
        {
            if (expires != null && notBefore != null)
            {
                if (DateTime.UtcNow < expires.Value.ToUniversalTime() & DateTime.UtcNow > notBefore.Value.ToUniversalTime())
                {
                    return true; 
                }
            }
            return false; 
        };
    }
}