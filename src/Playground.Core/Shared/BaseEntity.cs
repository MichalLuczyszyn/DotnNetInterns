namespace Playground.Core.Shared;

public class BaseEntity
{
    public BaseEntity()
    {
        Status = EntryStatus.Active;
    }
    internal EntryStatus Status { get; private set; }

    protected void Delete()
    {
        Status = EntryStatus.Deleted;
    }
}