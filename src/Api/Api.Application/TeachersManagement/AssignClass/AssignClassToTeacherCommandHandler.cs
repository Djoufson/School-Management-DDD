using Api.Application.Common;
using Api.Application.Common.Errors;
using Api.Application.Repositories;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Application.TeachersManagement.AssignClass;

public class AssignClassToTeacherCommandHandler : IRequestHandler<AssignClassToTeacherCommand, Result<TeacherResponse>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignClassToTeacherCommandHandler(
        IAdminRepository teacherRepository,
        IUnitOfWork unitOfWork)
    {
        _adminRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TeacherResponse>> Handle(AssignClassToTeacherCommand request, CancellationToken cancellationToken)
    {
        var adminId = AdminId.Create(request.AdminId);
        var teacherId = TeacherAdvisorId.Create(request.TeacherId);
        var classId = SchoolClassId.Create(request.ClassId);

        var admin = await _adminRepository.GetByIdAsync(adminId, cancellationToken);
        if(admin is null)
            return Result.Fail(new UserNotFoundError(request.AdminId));

        var teacher = admin.AssignTeacherToClass(teacherId, classId);
        if(teacher is null)
            return Result.Fail(new AssignmentFailedError());

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new TeacherResponse(teacher.Id.Value, teacher.FirstName, teacher.LastName, teacher.Password.Hash);
    }
}
