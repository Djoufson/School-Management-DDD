namespace Api.Application.TeachersManagement.AssignClass;

public record AssignTeacherToClassCommand(
    string AdminId,
    string TeacherId,
    string ClassId
) : IRequest<Result<TeacherResponse>>;
