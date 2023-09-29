using Api.Application.Repositories;
using Api.Domain.SchoolAggregate.Entities;
using Api.Domain.SchoolAggregate.ValueObjects;
using Api.Infrastructure.Persistance.Base;

namespace Api.Infrastructure.Persistance.Repositories;

public class TeacherRepository : Repository<TeacherAdvisor, UserId>, ITeacherRepository
{
    public TeacherRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<TeacherAdvisor?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await _context.TeacherAdvisors
            .Include(t => t.Classes)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
