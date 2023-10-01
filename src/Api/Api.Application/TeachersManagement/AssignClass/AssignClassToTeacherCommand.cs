namespace Api.Application.TeachersManagement.AssignClass;

public record AssignClassToTeacherCommand(
    string AdminId,
    string TeacherId,
    string ClassId
) : IRequest<Result<TeacherResponse>>;
