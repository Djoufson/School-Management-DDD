using Api.Domain.Common.Models;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public sealed class Seat : Entity<SeatId>
{
    public Student Student { get; private set; }
    public SchoolClass Class { get; private set; }
    private Seat(Student student, SchoolClass @class)
    {
        Student = student;
        Class = @class;
    }

    #pragma warning disable CS8618
    private Seat()
    {
    }
    #pragma warning restore CS8618

    public static Seat Create(Student student, SchoolClass @class)
    {
        return new(student, @class);
    }
}
