using Api.Application.ClassesManagement.Errors;
using Api.Application.Common;
using Api.Application.Common.Errors;
using Api.Application.Repositories;
using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Application.ClassesManagement.AddStudent;

public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, Result<ClassResponse>>
{
    private readonly IStudentsRepository _studentsRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddStudentCommandHandler(
        IStudentsRepository studentsRepository,
        ITeacherRepository teacherRepository,
        IUnitOfWork unitOfWork)
    {
        _studentsRepository = studentsRepository;
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ClassResponse>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var teacherId = TeacherAdvisorId.Create(request.TeacherId);
        var studentId = StudentId.Create(request.StudentId);
        var classId = SchoolClassId.Create(request.ClassId);

        var student = await _studentsRepository.GetByIdAsync(studentId, cancellationToken);
        var teacher = await _teacherRepository.GetByIdAsync(teacherId, cancellationToken);

        teacher!.AddStudent(classId, student!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ClassResponse(
            "",
            2019,
            (int)Specialization.DataScience,
            new ClassUser("", "", "", "", ""),
            Array.Empty<ClassUser>());

        // if (student is null)
        //     return Result.Fail(new UserNotFoundError(request.StudentId));

        // if (teacher is null)
        //     return Result.Fail(new UserNotFoundError(request.TeacherId));

        // var @class = teacher.AddStudent(classId, student);
        // await _unitOfWork.SaveChangesAsync(cancellationToken);

        // if (@class is null)
        //     return Result.Fail(new ClassNotFoundError(request.ClassId));

        // return new ClassResponse(
        //     @class.Id.Value,
        //     @class.Year,
        //     (int) @class.Specialization,
        //     @class.TeacherAdvisor is not null ?
        //         new ClassUser(
        //             @class.TeacherAdvisor?.Id.Value!,
        //             @class.TeacherAdvisor?.FirstName,
        //             @class.TeacherAdvisor?.LastName!,
        //             @class.TeacherAdvisor?.Password.Hash!,
        //             @class.TeacherAdvisor?.Role!)
        //         : null,
        //     @class.Students.Any() ?
        //         @class.Students.Select(s => new ClassUser(
        //             s.Id.Value,
        //             s.FirstName,
        //             s.LastName!,
        //             s.Password.Hash!,
        //             s.Role!)).ToList()
        //         : Array.Empty<ClassUser>());
    }
}
