namespace Playground.Core.Shared;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}