using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.Common.Models;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public class SchoolClass : Entity<SchoolClassId>
{
    private readonly Seat[] _seats;
    public int Year { get; }
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
        _seats = new Seat[seatsNumber];
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

    internal bool AddStudent(Student student)
    {
        // if(_students.Contains(student))
        //     return false;
    
        // _students.Add(student);
        return true;
    }

    internal bool RemoveStudent(Student student)
    {
        // _students.Add(student);
        // student.RemoveClass(this);
        return true;
    }

    internal bool AddRangeStudents(IEnumerable<Student> students)
    {
        // _students.AddRange(students);
        foreach (var student in students)
        {
            // student.AddClass(this);
        }
        return true;
    }
}
