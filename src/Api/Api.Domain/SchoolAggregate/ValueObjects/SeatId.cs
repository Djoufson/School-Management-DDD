using Api.Domain.Common.Models;

namespace Api.Domain.SchoolAggregate.ValueObjects;

public sealed class SeatId : ValueObject
{
    public Guid Id { get; private set; }

    private SeatId(Guid id)
    {
        Id = id;
    }

    public static SeatId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComparer()
    {
        yield return Id;
    }
}
