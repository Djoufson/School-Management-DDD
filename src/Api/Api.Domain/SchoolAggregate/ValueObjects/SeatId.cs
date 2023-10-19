using Api.Domain.Common.Models;

namespace Api.Domain.SchoolAggregate.ValueObjects;

public sealed class SeatId : ValueObject
{
    public Guid Value { get; private set; }

    private SeatId(Guid value)
    {
        Value = value;
    }

    public static SeatId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComparer()
    {
        yield return Value;
    }
}
