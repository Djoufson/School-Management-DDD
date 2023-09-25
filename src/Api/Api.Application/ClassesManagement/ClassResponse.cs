namespace Api.Application.ClassesManagement;

public record ClassResponse(
    string ClassId,
    int Year,
    int Specialization,
    ClassUser? TeacherAdvisor,
    IReadOnlyList<ClassUser> Students);

public record ClassUser(
    string Identifier,
    string? FirstName,
    string LastName,
    string Password,
    string Role);
