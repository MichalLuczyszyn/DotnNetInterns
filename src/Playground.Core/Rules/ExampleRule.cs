using Playground.Core.Shared;

namespace Playground.Core.Rules;

public class ExampleRule : IBusinessRule
{
    private readonly DateTimeOffset _dateTimeOffset;

    public ExampleRule(DateTimeOffset dateTimeOffset)
    {
        _dateTimeOffset = dateTimeOffset;
    }

    public bool IsBroken() => _dateTimeOffset > new DateTimeOffset(2050, 12, 1, 1, 1, 1, new TimeSpan(0, 1, 0, 0));

    public string Message => "Action cannot take place after 1.12.2050y";
}