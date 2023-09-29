using Api.Application.Repositories;
using Api.Domain.SchoolAggregate.Entities;
using Api.Domain.SchoolAggregate.ValueObjects;
using Api.Infrastructure.Persistance.Base;

namespace Api.Infrastructure.Persistance.Repositories;

public class StudentsRepository : Repository<Student, UserId>, IStudentsRepository
{
    public StudentsRepository(AppDbContext context) : base(context)
    {
    }
}
