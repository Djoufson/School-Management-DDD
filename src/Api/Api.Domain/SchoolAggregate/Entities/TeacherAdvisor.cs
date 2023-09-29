using Api.Domain.AcademicAggregate.Entities;
using Api.Domain.Common.Utilities;
using Api.Domain.Common.ValueObjects;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

// Manages the students of his class
public class TeacherAdvisor : User
{
    private readonly List<SchoolClass> _classes = new();
    public IReadOnlyList<SchoolClass> Classes => _classes.AsReadOnly();
    private TeacherAdvisor(
        TeacherAdvisorId id,
        string? firstName,
        string lastName,
        Password password,
        string role) : base(id, firstName, lastName, password, role)
    {
    }

    private TeacherAdvisor()
    {
    }

    internal bool AssignClass(SchoolClass @class)
    {
        // Validation checks
        _classes.Add(@class);
        @class.ChangeTeacher(this);
        return true;
    }

    internal bool UnAssignClass(SchoolClass @class)
    {
        // Verification checks
        _classes.Remove(@class);
        @class.ChangeTeacher(null);
        return true;
    }

    #region Students Management Concerns
    public SchoolClass? AddStudent(SchoolClassId classId, Student student)
    {
        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        @class?.AddStudent(student);
        return @class;
    }

    public SchoolClass? AddRangeStudents(SchoolClassId classId, IEnumerable<Student> students)
    {
        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        @class?.AddRangeStudents(students);
        return @class;
    }

    public SchoolClass? RemoveStudent(SchoolClassId classId, Student student)
    {
        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        @class?.RemoveStudent(student);
        return @class;
    }

    public bool NoteStudent(
        Discipline discipline,
        StudentId studentId,
        int year,
        decimal note)
    {
        var @class = _classes
            .FirstOrDefault(c => c.Year == year && c.Students.Any(s => s.Id == studentId));

        var student = @class?.Students.FirstOrDefault(s => s.Id == studentId);
        if(student is null)
            return false;

        student.AddNotation(
            Notation.CreateUnique(student, note, discipline));

        return true;
    }
    #endregion

    public static TeacherAdvisor CreateUnique(
        string? firstName,
        string lastName,
        Password password,
        int year)
    {
        return new(
            TeacherAdvisorId.CreateUnique(year),
            firstName,
            lastName,
            password,
            Roles.Teacher);
    }

    internal static TeacherAdvisor Create(
        TeacherAdvisorId id,
        string? firstName,
        string lastName,
        Password password)
    {
        return new(
            id,
            firstName,
            lastName,
            password,
            Roles.Teacher);
    }
}
