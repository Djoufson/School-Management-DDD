using Api.Application.Common;
using Api.Domain.Common.ValueObjects;
using Api.Domain.SchoolAggregate.Entities;
using Api.Infrastructure.Persistance;
using Api.Presentation.Middlewares;
using Carter;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Api.Presentation;

public static class Startup
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<RegisterUserIdMiddleware>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("oauth", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        });
        return services;
    }

    public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Add default admin if there are no one
        bool exists = await context.Admins.AnyAsync();
        if (!exists)
        {
            Admin admin = Admin.CreateUnique("Admin", "Admin", Password.CreateNewPassword("string"), DateTime.Now.Year);
            await context.Admins.AddAsync(admin);
            await unitOfWork.SaveChangesAsync();
        }

        return app;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<RegisterUserIdMiddleware>();

        app.MapCarter();

        return app;
    }
}
