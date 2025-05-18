using System;
using System.IO;
using System.Threading.Tasks;
using FriendNote.Infrastructure;

namespace FriendNote.Configuration
{

    /// <summary>
    /// Служит для предоставления информациив ConfigurationService
    /// </summary>
    static class ConfigurationAgent
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
