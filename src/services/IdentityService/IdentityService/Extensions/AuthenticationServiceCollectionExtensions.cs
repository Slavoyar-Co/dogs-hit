using IdentityService.Implementations;
using IdentityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityService.ServiceExtensions
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationProviders(this IServiceCollection services, string key)
        {
            services.AddSingleton<IJwtManager, JwtManager>();
            services.AddSingleton<IGoogleAuthManager, GoogleAuthManager>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

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
            })
            .AddGoogle(options =>
            {
                options.ClientId = "922665812793-7cbic7c0807hcqljk06q7k9mtdet21p2.apps.googleusercontent.com";
                options.ClientSecret = "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";
            }); 




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