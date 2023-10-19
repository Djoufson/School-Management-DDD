﻿using Api.Domain.AcademicAggregate.Enums;
using Api.Domain.Common.Utilities;
using Api.Domain.Common.ValueObjects;
using Api.Domain.SchoolAggregate.ValueObjects;

namespace Api.Domain.SchoolAggregate.Entities;

public class Admin : User
{
    // The admin should be able to create and dissolve classes, so he should have the list of all existing classes
    // Actually, from the user base class he already have all students

    private readonly List<Student> _students = new();
    private readonly List<TeacherAdvisor> _teachers = new();
    private readonly List<SchoolClass> _classes = new();

    public IReadOnlyList<SchoolClass> Classes => _classes.AsReadOnly();
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<TeacherAdvisor> Teachers => _teachers.AsReadOnly();

    public Admin(
        AdminId id,
        string? firstName,
        string lastName,
        Password password,
        string role) : base(id, firstName, lastName, password, role)
    {
    }

    private Admin()
    {
    }

    public static Admin CreateUnique(
        string? firstName,
        string lastName,
        Password password,
        int year)
    {
        return new(
            AdminId.CreateUnique(year),
            firstName,
            lastName,
            password,
            Roles.Admin);
    }

    #region Classes Administration concerns
    public SchoolClass CreateUniqueClass(
        Admin admin,
        TeacherAdvisor? teacherAdvisor,
        Specialization specialization,
        int year,
        int seatsNumber)
    {
        var @class = SchoolClass.CreateUnique(
            admin,
            specialization,
            teacherAdvisor,
            year,
            seatsNumber);

        _classes.Add(@class);
        return @class;
    }

    public SchoolClass CreateClass(
        Admin admin,
        TeacherAdvisorId? teacherId,
        Specialization specialization,
        int year,
        int seatsNumber)
    {
        var teacher = Teachers.FirstOrDefault(t => t.Id == teacherId);
        var @class = SchoolClass.CreateUnique(
            admin,
            specialization,
            teacher,
            year,
            seatsNumber);

        teacher?.AssignClass(@class);
        _classes.Add(@class);
        return @class;
    }

    public bool DissolveClass(SchoolClassId schoolClassId)
    {
        var @class = _classes.FirstOrDefault(c => c.Id == schoolClassId);
        if (@class is null)
            return false;

        @class.ChangeTeacher(null);
        @class.CleanSeats();

        return true;
    }
    #endregion

    #region Student Administration concerns

    public Student RegisterStudent(
        string? firstName,
        string lastName,
        DateTime dateOfBirth,
        int level,
        Password password,
        int year,
        Specialization? specialization)
    {
        var student = Student.CreateUnique(
            firstName,
            lastName,
            dateOfBirth,
            level,
            password,
            year,
            specialization);

        _students.Add(student);
        return student;
    }

    #endregion

    #region Teacher administration concerns

    public TeacherAdvisor? AssignTeacherToClass(
        TeacherAdvisorId teacherId,
        SchoolClassId classId)
    {
        var teacher = Teachers.FirstOrDefault(t => t.Id == teacherId);
        if(teacher is null)
            return null;

        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        if(@class is null)
            return null;

        teacher.AssignClass(@class);
        @class.ChangeTeacher(teacher);
        return teacher;
    }

    public bool UnassignTeacherToClass(
        TeacherAdvisorId teacherId,
        SchoolClassId classId)
    {
        var teacher = Teachers.FirstOrDefault(t => t.Id == teacherId);
        if(teacher is null)
            return false;

        var @class = Classes.FirstOrDefault(c => c.Id == classId);
        if(@class is null)
            return false;

        @class.ChangeTeacher(null);
        return teacher.UnAssignClass(@class);
    }

    public TeacherAdvisor RegisterTeacher(
        string? firstName,
        string lastName,
        Password password,
        int year)
    {
        var teacher = TeacherAdvisor.CreateUnique(
            firstName,
            lastName,
            password,
            year);

        _teachers.Add(teacher);
        return teacher;
    }
    #endregion
}
