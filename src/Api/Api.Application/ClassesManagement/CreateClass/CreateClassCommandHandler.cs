using Api.Application.Common;
using Api.Application.Common.Errors;
using Api.Application.Repositories;
using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Application.ClassesManagement.CreateClass;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<ClassResponse>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClassCommandHandler(
        IAdminRepository adminRepository,
        IUnitOfWork unitOfWork)
    {
        _adminRepository = adminRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ClassResponse>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var adminId = AdminId.Create(request.AdminId);
        TeacherAdvisorId? teacherAdvisorId = null;
        if(request.TeacherAdvisorId is not null)
            teacherAdvisorId = TeacherAdvisorId.Create(request.TeacherAdvisorId);

        var admin = await _adminRepository.GetByIdAsync(adminId, cancellationToken);
        if(admin is null)
            return Result.Fail(new UserNotFoundError(request.AdminId));

        var @class = admin.CreateClass(
            admin,
            teacherAdvisorId,
            (Specialization)request.Specialization,
            request.Year);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ClassResponse(
            @class.Id.Value,
            @class.Year,
            (int)@class.Specialization,
            @class.TeacherAdvisor is not null ? 
                new ClassUser(
                    @class.TeacherAdvisor?.Id.Value!,
                    @class.TeacherAdvisor?.FirstName,
                    @class.TeacherAdvisor?.LastName!,
                    @class.TeacherAdvisor?.Password.Hash!,
                    @class.TeacherAdvisor?.Role!) 
                : null,
            @class.Students.Any() ? 
                @class.Students.Select(s => new ClassUser(
                    s.Id.Value,
                    s.FirstName,
                    s.LastName!,
                    s.Password.Hash!,
                    s.Role!)).ToList()
                : Array.Empty<ClassUser>()
        );
    }
}
