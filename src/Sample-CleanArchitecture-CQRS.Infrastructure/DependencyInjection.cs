using System.Reflection;
using System.Text;
using System.Text.Json;

using Mapster;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
using Sample_CleanArchitecture_CQRS.Infrastructure.Configuration;
using Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Settings;
using Sample_CleanArchitecture_CQRS.Infrastructure.Data;
using Sample_CleanArchitecture_CQRS.Infrastructure.Generators;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;
using Sample_CleanArchitecture_CQRS.Infrastructure.Repositories;
using Sample_CleanArchitecture_CQRS.Infrastructure.Resources.Results;
using Sample_CleanArchitecture_CQRS.Infrastructure.Services.Identity;
using Sample_CleanArchitecture_CQRS.Infrastructure.Services.Interafaces;

namespace Sample_CleanArchitecture_CQRS.Infrastructure;

public static class DependencyInjection
{ 
    public static IServiceCollection AddInfrastructre(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddAuthentication(configurationManager)
                .AddIdentity()
                .AddDbContext(configurationManager)
                .AddInfrastructureMapping();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();



        return services;

    }

    internal static IServiceCollection AddAuthentication(this IServiceCollection services, 
        ConfigurationManager configurationManager)
    {
        JwtConfig? jwtConfig = configurationManager.GetSection(JwtConfig.SectionName)?.Get<JwtConfig>();

        if (jwtConfig is null)
        {
            throw new ArgumentException("JwtConfig is Not Provided On Settings");
        }

        services.AddSingleton(Options.Create(jwtConfig));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfig.Secret))
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    var json = JsonSerializer.Serialize(ApiResult<string>.Failed(JwtErrors.UnAuthorized));

                    return context.Response.WriteAsync(json);
                }
            };
                
        });

        return services;
    }

    internal static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<ApplicationUser>()
             .AddRoles<ApplicationRole>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), services);

        builder.AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthentication();

        services.AddScoped<SignInManager<ApplicationUser>>();

        return services;
    }

    internal static IServiceCollection AddDbContext(this IServiceCollection services, 
        ConfigurationManager configurationManager)
    {
        ConnectionStrings? dbConfig = configurationManager.GetSection(ConnectionStrings.SectionName).Get<ConnectionStrings>();
        if (dbConfig is null)
        {
            throw new ArgumentException("DbConfig is Not Provided On Settings");
        }


        services.AddDbContext<AppDbContext>(s =>
        {
            // No Usage For Other Properties of DbConfig For Now!
            s.UseSqlite(dbConfig.DefaultConnection);
        });

        return services;
    }

}