using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoListApp.Auth.Models;
using ToDoListApp.Auth.Services;

namespace ToDoListApp.Dal;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetSection("Jwt").Get<AuthOptions>();
        services.AddSingleton<AuthOptions>(authOptions);

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ClockSkew = new TimeSpan(0,1,0)
                }
        );

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
