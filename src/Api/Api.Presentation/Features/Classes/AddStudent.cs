using Api.Application.ClassesManagement.AddStudent;
using Api.Domain.Common.Utilities;
using Api.Presentation.Utilities;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Features.Classes;

public class AddStudent : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPut("classes/{classId}", Handle)
            .RequireAuthorization(Policies.TeacherOnly);
    }

    private async Task<IResult> Handle(
        HttpContext context,
        [FromRoute] string classId,
        AddStudentRequest request,
        ISender sender)
    {
        var teacherId = AuthHeaders.GetUserId(context.Request.Headers);
        var command = new AddStudentCommand(
            teacherId,
            classId,
            request.StudentId);

        var response = await sender.Send(command);
        return Results.Ok(response.Value);
    }

    public record AddStudentRequest(
        string StudentId
    );
}
