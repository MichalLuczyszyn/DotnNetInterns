namespace Playground.Application.Clock;

public interface IClock
{
    DateTime Current();
    DateTimeOffset CurrentOffset();
}