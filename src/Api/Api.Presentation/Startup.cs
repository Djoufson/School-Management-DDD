using Api.Application.Common;
using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.Common.ValueObjects;
using Api.Domain.SchoolAggregate.Entities;
using Api.Domain.SchoolAggregate.ValueObjects;
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
        // UserId studentId = null!;
        // UserId teacherId = null!;
        // SchoolClassId classId = null!;
        bool exists = true;
        using var scope = app.ApplicationServices.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Add default admin if there are no one
        exists = await context.Admins.AnyAsync();
        if (!exists)
        {
            Admin admin = Admin.CreateUnique("Admin", "Admin", Password.CreateNewPassword("string"), DateTime.Now.Year);
            var teacher = admin.RegisterTeacher("Teacher", "Teacher", Password.CreateNewPassword("string"), 2012);
            var studentA = admin.RegisterStudent("Student", "Student", DateTime.Now, 1, Password.CreateNewPassword("string"), 2019, Specialization.BigData);
            // var studentB = admin.RegisterStudent("StudentB", "StudentB", DateTime.Now, 1, Password.CreateNewPassword("string"), 2019, Specialization.BigData);
            var @class = admin.CreateClass(admin, null, Specialization.DataScience, 2023, 50);
            admin.AssignTeacherToClass((TeacherAdvisorId)teacher.Id, @class.Id);
            teacher.AddStudent(@class.Id, studentA);

            await context.Admins.AddAsync(admin);
            await unitOfWork.SaveChangesAsync();

            // classId = @class.Id;
            // studentId = studentB.Id;
            // teacherId = teacher.Id;
        }

        // using (var scope = app.ApplicationServices.CreateAsyncScope())
        // {
        //     if(!exists)
        //     {
        //         var studentsRepository = scope.ServiceProvider.GetRequiredService<IStudentsRepository>();
        //         var teacherRepository = scope.ServiceProvider.GetRequiredService<ITeacherRepository>();
        //         var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        //         var student = await studentsRepository.GetByIdAsync(studentId);
        //         var teacher = await teacherRepository.GetByIdAsync(teacherId);

        //         teacher!.AddStudent(classId, student!);
        //         await unitOfWork.SaveChangesAsync();
        //     }
        // }
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
