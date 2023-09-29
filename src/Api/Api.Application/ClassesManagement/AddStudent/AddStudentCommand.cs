namespace Api.Application.ClassesManagement.AddStudent;

public record AddStudentCommand(
    string TeacherId,
    string ClassId,
    string StudentId) : IRequest<Result<ClassResponse>>;
