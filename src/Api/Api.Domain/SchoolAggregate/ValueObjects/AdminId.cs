﻿using Api.Domain.Common.Utilities;

namespace Api.Domain.SchoolAggregate.ValueObjects;

public class AdminId : UserId
{
    private const string _prefix = "Admin";

    public AdminId(
        string code,
        int year,
        int salt) : base(code, year, salt)
    {
    }

    protected override string Prefix => _prefix;
    public static new AdminId Create(string value)
    {
        var (year, code, salt) = Decrypt(value, _prefix);
        return new(code, year, salt);
    }

    internal static AdminId CreateUnique(int year)
    {
        return new(
            RandomCodeGenerator.GetRandomCode(3),
            year,
            Random.Shared.Next(10000, 99999));
    }
}
