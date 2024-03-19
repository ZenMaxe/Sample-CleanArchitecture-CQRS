using Mapster;

using Sample_CleanArchitecture_CQRS.Application;
using Sample_CleanArchitecture_CQRS.Infrastructure;

using Serilog;

namespace Sample_CleanArchitecture_CQRS.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();

        builder.Host.UseSerilog(
            (context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

        // Add services to the container.

        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMapster();
        builder.Services.AddHttpContextAccessor();
        builder.Services
                        .AddInfrastructre(builder.Configuration)
                        .AddAPI()
                        
                        .AddApplicationService();

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Clean With CQRS v1");
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}