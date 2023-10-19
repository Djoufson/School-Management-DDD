using Api.Application.Common;
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

        var seat = teacher!.AddStudent(classId, student!);

        if(seat is null)
            return null!;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ClassResponse(
            "",
            2019,
            (int)Specialization.DataScience,
            new ClassUser("", "", "", "", ""),
            Array.Empty<ClassUser>());
    }
}
