using System;
using System.IO;
using System.Threading.Tasks;
using FriendNote.Infrastructure;
using SQLite;

namespace FriendNote.Configuration
{
    public class ConfigurationService : IService, IDisposable
    {
        private ServiceStatus _status;
        private AppSettings _settings;

        public ServiceStatus Status
        {
            get => _status;
            private set
            {
                Logger.Log($"Configuration service is {value.ToString()}");
                _status = value;
            }
        }
        public SQLiteConnectionString ConnectionString { get; set; }


        public async void Startup()
        {
            Status = ServiceStatus.Initializing;

            _settings = await ConfigurationAgent.LoadAppSettingsAsync();
            ConnectionString = new(_settings.ConnectionString);
            Logger.Log($"Connnection string to database: {ConnectionString.DatabasePath}");

            Status = ServiceStatus.Started;
        }

        public void Dispose()
        {
            if (!ConfigurationAgent.SaveAppSettings(_settings))
            {
                Logger.LogError("Error when saving settings");
            }
        }


        /// <summary>
        /// Служит для предоставления информациив ConfigurationService
        /// </summary>
        private static class ConfigurationAgent
        {
            private static readonly string _appSettingsFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, "appsettings.xml");

            internal static async Task<AppSettings> LoadAppSettingsAsync()
            {
                try
                {
                    return await Serializer.DeserializeFromFileAsync<AppSettings>(_appSettingsFilePath);
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex.ToString());
                    Logger.Log("Create new application settings");
                    return new();
                }
            }

            internal static bool SaveAppSettings(AppSettings data)
            {
                try
                {
                    Serializer.SerializeToFile(data, _appSettingsFilePath);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    return false;
                }
            }
        }
    }
}
