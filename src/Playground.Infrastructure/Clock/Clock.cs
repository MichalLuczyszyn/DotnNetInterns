using Playground.Application.Clock;

namespace Playground.Infrastructure.Clock;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
    public DateTimeOffset CurrentOffset() => DateTimeOffset.UtcNow;
}