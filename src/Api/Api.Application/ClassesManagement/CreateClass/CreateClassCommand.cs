namespace Api.Application.ClassesManagement.CreateClass;

public record CreateClassCommand(
    string AdminId,
    int Specialization,
    string? TeacherAdvisorId,
    int Year
) : IRequest<Result<ClassResponse>>;
