using Invoice_Api.Auth;
using Invoice_Api.Data;
using Invoice_Api.Models.Humans;
using Invoice_Api.Services.Jwt_service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Invoice_Api;

public static class DI
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {

            setup.SwaggerDoc("v1", new OpenApiInfo 
            {
                Title = "Invoice",
                Version = "v2"
            });

            var filePath = Path.Combine(AppContext.BaseDirectory, "Invoice_Api.xml");

            setup.IncludeXmlComments(filePath);

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name= "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat= "JWT",
                In = ParameterLocation.Header,
                Description = "Jwt Authorization header using the Bearer sheme. \r\n"

            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[]{}
                }
            });
        });
            return services;
    }

    public static IServiceCollection AuthenticationAndAuthorization(
       this IServiceCollection services,
       IConfiguration configuration
       )
    {
        services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<InvoiceDbContext>();

        services.AddScoped<IJwtService, JwtService>();
        var jwtConfig = new JwtConfig();
        configuration.GetSection("JWT").Bind(jwtConfig);

        services.AddSingleton(jwtConfig);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters =
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfig.Secret)
                        )
                };
            });

        

        return services;
    }
}
