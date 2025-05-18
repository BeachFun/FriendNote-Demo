using System;
using System.IO;
using FriendNote.Core;
using SQLite;
using UnityEngine;

namespace FriendNote.Configuration
{
    public partial class ConfigurationService : ServiceBase, IDisposable
    {
        private SQLiteConnectionString _connectionString;

        public SQLiteConnectionString ConnectionString
        {
            get => _connectionString;
            private set
            {
                _connectionString = value;
                PlayerPrefs.SetString("ConnectionString", value.DatabasePath);
                print($"Connnection string to database: {value.DatabasePath}");
            }
        }


        #region [Методы] Запуск и инициализация сервиса

        public override void Startup()
        {
            Status.Value = ServiceStatus.Initializing;

            string connectionString = PlayerPrefs.HasKey("ConnectionString")
                ? PlayerPrefs.GetString("ConnectionString")
                : Path.Combine(Application.persistentDataPath, "friend-note.db");
            ConnectionString = new(connectionString);

            Status.Value = ServiceStatus.Started;
        }

        public void Dispose()
        {

        }

        #endregion


        /// <summary>
        /// Уставливет путь к бд по-умолчанию.
        /// </summary>
        public void SetDatabasePath() => ConnectionString = new(Path.Combine(Application.persistentDataPath, "friend-note.db"));

        /// <summary>
        /// Сохранение нового пути к базе данных. Если передать путь к несуществующей БД, то она создастся.
        /// </summary>
        /// <param name="path">Путь и название БД</param>
        public void SetDatabasePath(string path) => ConnectionString = new(path);
    }
}
