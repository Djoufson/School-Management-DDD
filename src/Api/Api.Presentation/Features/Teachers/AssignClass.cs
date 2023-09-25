using Api.Application.TeachersManagement.AssignClass;
using Api.Domain.Common.Utilities;
using Api.Presentation.Utilities;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Features.Teachers;

public class AssignClass : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPut("teacher/{TeacherId}", Handle)
            .RequireAuthorization(Policies.AdminOnly);
    }

    private async Task<IResult> Handle(
        HttpContext context,
        [FromRoute] string teacherId,
        [FromBody] AssignClassRequest request,
        ISender sender)
    {
        var adminId = AuthHeaders.GetUserId(context.Request.Headers);
        var command = new AssignTeacherToClassCommand(adminId, teacherId, request.ClassId);
        var response = await sender.Send(command);
        return Results.Ok(response.Value);
    }

    public record AssignClassRequest(
        string ClassId
    );
}
