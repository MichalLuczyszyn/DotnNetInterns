using Playground.Core.Exceptions;

namespace Playground.Core.ValueObjects;

public sealed record DeskSpotId
{
    private DeskSpotId()
    {
        
    }
    public Guid Value { get; }

    public DeskSpotId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator Guid(DeskSpotId date) => date.Value;
    
    public static implicit operator DeskSpotId(Guid value) => new(value);
}