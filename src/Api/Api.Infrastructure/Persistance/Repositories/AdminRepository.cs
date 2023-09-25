using Api.Application.Repositories;
using Api.Domain.SchoolAggregate.Entities;
using Api.Domain.SchoolAggregate.ValueObjects;
using Api.Infrastructure.Persistance.Base;

namespace Api.Infrastructure.Persistance.Repositories;

public class AdminRepository : Repository<Admin, UserId>, IAdminRepository
{
    public AdminRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Admin?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await _context.Admins
            .Include(a => a.Teachers)
            .Include(a => a.Students)
            .Include(a => a.Classes)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
