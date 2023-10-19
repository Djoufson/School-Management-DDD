using Carter;
using Api.Application.ClassesManagement.CreateClass;
using Api.Presentation.Utilities;
using Api.Domain.Common.Utilities;

namespace Api.Presentation.Features.Classes;

public class CreateClass : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("classes", Handle)
            .RequireAuthorization(Policies.AdminOnly);
    }

    private async Task<IResult> Handle(HttpContext context, CreateClassRequest request, ISender sender)
    {
        var adminId = AuthHeaders.GetUserId(context.Request.Headers);
        var command = new CreateClassCommand(
            adminId,
            request.Specialization,
            request.TeacherAdvisorId,
            request.Year,
            request.SeatsNumber);

        var response = await sender.Send(command);
        return Results.Ok(response.Value);
    }

    public record CreateClassRequest(
        int Specialization,
        string? TeacherAdvisorId,
        int Year,
        int SeatsNumber);
}
