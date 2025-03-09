namespace FriendNote
{
    public interface IController
    {
        ServiceStatus Status { get; }
    }
    public enum ServiceStatus
    {
        Shutdown,
        Initializing,
        Started
    }
}
