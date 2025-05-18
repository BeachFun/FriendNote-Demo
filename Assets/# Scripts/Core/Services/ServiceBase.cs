using UniRx;
using UnityEngine;

namespace FriendNote.Core
{
    public abstract class ServiceBase : MonoBehaviour, IService
    {
        public ReactiveProperty<ServiceStatus> Status { get; } = new();

        /// <summary>
        /// Запуск сервиса
        /// </summary>
        public abstract void Startup();

        protected virtual void Awake()
        {
            Status.Subscribe(OnStatusUpdated).AddTo(this);
            Startup();
        }

        private void OnStatusUpdated(ServiceStatus status)
        {
            print($"{name} is {status.ToString()}");
        }
    }
}
