using System.Collections.Generic;
using System.Threading.Tasks;
using FriendNote.Configuration;
using FriendNote.Data;
using FriendNote.Infrastructure;
using Observer;

namespace FriendNote
{
    /// <summary>
    /// Медиатор, связующий все сервисы слоев
    /// </summary>
    public static class Services
    {
        private static List<IService> _startSequence;

        public static ConfigurationService Config { get; private set; }
        public static EntityDataService EntityData { get; private set; }
        public static DataService Data { get; private set; }


        /// <summary>
        /// Инициализация и запуск сервисов
        /// </summary>
        public static async void Initialize()
        {
            if (Config != null)
            {
                Messenger.Broadcast(Notices.StartupNotice.ALL_SERVICES_STARTED);
                return;
            }

            // Инициализация сервисов
            Config = new();
            EntityData = new();
            Data = new();

            _startSequence = new List<IService>
            {
                Config,
                EntityData,
                Data
            };

            // Запуск сервисов
            await StartupServices();
        }

        /// <summary>
        /// Ассинхронный запуск всех сервисов, привязанных к этому классу
        /// </summary>
        private static async Task StartupServices()
        {
            Logger.Log("Servieces startup...");

            foreach (IService manager in _startSequence)
            {
                manager.Startup();
            }

            await Task.Yield();

            int numModules = _startSequence.Count;
            int numReady = 0;

            while (numReady < numModules)
            {
                int lastReady = numReady;
                numReady = 0;

                foreach (IService manager in _startSequence)
                {
                    if (manager.Status == ServiceStatus.Started)
                    {
                        numReady++;
                    }
                }

                if (numReady > lastReady)
                {
                    Logger.Log("Progress: " + numReady + "/" + numModules);
                }

                await Task.Yield();
            }

            _startSequence = null;
            Logger.Log("All services started up");
            Messenger.Broadcast(Notices.StartupNotice.ALL_SERVICES_STARTED);
        }
    }
}
