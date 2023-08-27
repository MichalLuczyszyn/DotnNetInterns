using Playground.Core.Exceptions;

namespace Playground.Core.ValueObjects;

public sealed record RoomId
{
    public Guid Value { get; }

    private RoomId()
    {
        
    }
    public RoomId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator Guid(RoomId date) => date.Value;
    
    public static implicit operator RoomId(Guid value) => new(value);
}