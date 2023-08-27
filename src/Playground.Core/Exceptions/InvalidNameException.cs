namespace Playground.Core.Exceptions;

public sealed class InvalidNameException : CustomException
{
    public string FullName { get; }

    public InvalidNameException(string fullName) : base($"Name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}