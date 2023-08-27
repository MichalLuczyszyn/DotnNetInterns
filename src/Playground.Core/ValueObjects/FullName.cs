using Playground.Core.Exceptions;

namespace Playground.Core.ValueObjects;

public sealed record FullName
{
    private FullName()
    {
        
    }
    public string Value { get; }
        
    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 1)
        {
            throw new InvalidFullNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator FullName(string value) => value is null ? null : new FullName(value);

    public static implicit operator string(FullName value) => value?.Value;

    public override string ToString() => Value;
}