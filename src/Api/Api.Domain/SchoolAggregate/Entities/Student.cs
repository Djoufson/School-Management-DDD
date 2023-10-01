using Api.Domain.AcademicAggregate.Entities;
using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.AcademicAggregate.ValueObjects;
using Api.Domain.Common.Utilities;
using Api.Domain.Common.ValueObjects;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public class Student : User
{
    private readonly List<Notation> _notations = new();
    private readonly List<SchoolClass> _classes = new();
    public DateTime DateOfBirth { get; }
    public int Level { get; private set; }
    public Specialization? Specialization { get; private set; }

    public IReadOnlyList<Notation> Notations => _notations.AsReadOnly();
    public IReadOnlyList<SchoolClass> Classes => _classes.AsReadOnly();

    private Student(
        StudentId id,
        string? firstName,
        string lastName,
        Password password,
        DateTime dateOfBirth,
        int level,
        Specialization? specialization,
        string role) : base(id, firstName, lastName, password, role)
    {
        DateOfBirth = dateOfBirth;
        Level = level;
        Specialization = specialization;
    }

    private Student()
    {
    }

    internal bool AddClass(SchoolClass @class)
    {
        if(_classes.Contains(@class))
            return false;
        _classes.Add(@class);
        return true;
    }

    internal bool RemoveClass(SchoolClass @class)
    {
        return _classes.Remove(@class);
    }

    internal bool AddNotation(Notation notation)
    {
        _notations.Add(notation);
        return true;
    }

    internal void UpdateNotation(NotationId notationId, decimal? value)
    {
        var notation = _notations.FirstOrDefault(n => n.Id == notationId);
        if(notation is null)
            return;

        notation.UpdateValue(value);
    }

    internal static Student CreateUnique(
        string? firstName,
        string lastName,
        DateTime dateOfBirth,
        int level,
        Password password,
        int year,
        Specialization? specialization)
    {
        return new(
            StudentId.CreateUnique(year),
            firstName,
            lastName,
            password,
            dateOfBirth,
            level,
            specialization,
            Roles.Student);
    }

    public static Student Create(
        StudentId id,
        string? firstName,
        string lastName,
        DateTime dateOfBirth,
        int level,
        Password password,
        Specialization? specialization)
    {
        return new(
            id,
            firstName,
            lastName,
            password,
            dateOfBirth,
            level,
            specialization,
            Roles.Student);
    }
}
