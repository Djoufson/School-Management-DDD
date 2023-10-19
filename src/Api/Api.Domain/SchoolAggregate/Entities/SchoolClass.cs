using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.Common.Models;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public class SchoolClass : Entity<SchoolClassId>
{
    private readonly List<Seat> _seats = new();
    public int AvailableSeats { get; private set; }
    public int TotalSeats { get; private set; }
    public int Year { get; private set; }
    public Specialization Specialization { get; }
    public TeacherAdvisor? TeacherAdvisor { get; private set; }
    public IReadOnlyList<Seat> Seats => _seats.AsReadOnly();
    public Admin Admin { get; internal set; }

    private SchoolClass(
        SchoolClassId id,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        int year,
        Admin admin,
        int seatsNumber) : base(id)
    {
        TeacherAdvisor = teacherAdvisor;
        Specialization = specialization;
        Year = year;
        Admin = admin;
        TotalSeats = seatsNumber;
        AvailableSeats = seatsNumber;
    }

#pragma warning disable CS8618
    private SchoolClass()
    {
    }
#pragma warning restore CS8618

    internal static SchoolClass CreateUnique(
        Admin admin,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        int year,
        int seatsNumber)
    {
        return new(
            SchoolClassId.CreateUnique(specialization, year),
            specialization,
            teacherAdvisor,
            year,
            admin,
            seatsNumber);
    }

    internal static SchoolClass Create(
        Admin admin,
        SchoolClassId schoolClassId,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        int year,
        int seatsNumber)
    {
        return new(
            schoolClassId,
            specialization,
            teacherAdvisor,
            year,
            admin,
            seatsNumber);
    }

    internal void ChangeTeacher(TeacherAdvisor? teacher)
    {
        TeacherAdvisor = teacher;
    }

    internal Seat? AddStudent(Student student)
    {
        if(AvailableSeats <= 0)
            return null;

        bool exist = _seats.Any(s => s.Student == student);
        if(exist)
            return null;

        var seat = Seat.Create(student, this);
        _seats.Add(seat);
        AvailableSeats--;
        return seat;
    }

    internal bool RemoveStudent(Student student)
    {
        if(AvailableSeats == TotalSeats)
            return false;

        var toRemoveSeats = _seats.Where(s => s.Student == student).ToArray();
        int i = 0;
        while(toRemoveSeats.Any())
        {
            _seats.Remove(toRemoveSeats[i]);
        }

        AvailableSeats++;
        return true;
    }

    internal bool CleanSeats()
    {
        foreach(var seat in _seats)
            seat.Student.RemoveSeat(seat);

        _seats.Clear();
        return true;
    }
}
