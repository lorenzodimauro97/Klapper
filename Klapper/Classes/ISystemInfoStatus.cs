namespace Klapper.Shared.Components;

public interface ISystemInfoStatus
{
    public string active_state { get; set; }
    public string sub_state { get; set; }
}

public class SystemInfoStatus : ISystemInfoStatus
{
    public string active_state { get; set; }
    public string sub_state { get; set; }
}