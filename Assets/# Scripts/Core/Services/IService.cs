using UniRx;

namespace FriendNote.Core
{
    public interface IService
    {
        ReactiveProperty<ServiceStatus> Status { get; }
        void Startup();
    }

    public enum ServiceStatus
    {
        Shutdown,
        Initializing,
        Started,
        Suspended
    }
}
