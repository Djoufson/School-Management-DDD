﻿using Api.Domain.AcademicAggregate.Entities;
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
        if(_classes.Contains(@class))
            return false;

        _classes.Add(@class);
        return true;
    }

    internal bool UnAssignClass(SchoolClass @class)
        => _classes.Remove(@class);

    #region Students Management Concerns
    public Seat? AddStudent(SchoolClassId classId, Student student)
    {
        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        if(@class is null)
            return null;

        var seat = @class.AddStudent(student);
        if(seat is null)
            return null;

        student.GiveASeat(seat);
        return seat;
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
        // var @class = _classes
        //     .FirstOrDefault(c => c.Year == year && c.Students.Any(s => s.Id == studentId));

        // var student = @class?.Students.FirstOrDefault(s => s.Id == studentId);
        // if(student is null)
        //     return false;

        // student.AddNotation(
        //     Notation.CreateUnique(student, note, discipline));

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
