using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.Common.Models;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public class SchoolClass : Entity<SchoolClassId>
{
    private readonly List<Student> _students = new();

    public int Year { get; }
    public Specialization Specialization { get; }
    public TeacherAdvisor? TeacherAdvisor { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public Admin Admin { get; internal set; }

    private SchoolClass(
        SchoolClassId id,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        int year,
        Admin admin) : base(id)
    {
        TeacherAdvisor = teacherAdvisor;
        Specialization = specialization;
        Year = year;
        Admin = admin;
    }

    private SchoolClass(
        SchoolClassId id,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        IEnumerable<Student> students,
        int year,
        Admin admin) : base(id)
    {
        _students = students.ToList();
        TeacherAdvisor = teacherAdvisor;
        Specialization = specialization;
        Year = year;
        Admin = admin;
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
        int year)
    {
        return new(
            SchoolClassId.CreateUnique(specialization, year),
            specialization,
            teacherAdvisor,
            year,
            admin);
    }

    internal static SchoolClass Create(
        Admin admin,
        SchoolClassId schoolClassId,
        Specialization specialization,
        TeacherAdvisor? teacherAdvisor,
        int year)
    {
        return new(
            schoolClassId,
            specialization,
            teacherAdvisor,
            year,
            admin);
    }

    internal static SchoolClass Create(
        Admin admin,
        SchoolClassId schoolClassId,
        TeacherAdvisor? teacherAdvisor,
        Specialization specialization,
        IEnumerable<Student> students,
        int year)
    {
        return new(
            schoolClassId,
            specialization,
            teacherAdvisor,
            students,
            year,
            admin);
    }

    internal void ChangeTeacher(TeacherAdvisor? teacher)
    {
        TeacherAdvisor = teacher;
    }

    internal bool AddStudent(Student student)
    {
        _students.Add(student);
        student.AddClass(this);
        return true;
    }

    internal bool RemoveStudent(Student student)
    {
        _students.Add(student);
        student.RemoveClass(this);
        return true;
    }

    internal bool AddRangeStudents(IEnumerable<Student> students)
    {
        _students.AddRange(students);
        foreach (var student in students)
        {
            student.AddClass(this);
        }
        return true;
    }
}
