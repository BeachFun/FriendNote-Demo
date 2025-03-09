using System;
using System.IO;
using UnityEngine;

namespace FriendNote.Configuration
{
    [Serializable]
    public class AppSettings
    {
        public string ConnectionString { get; set; } = Path.Combine(Application.persistentDataPath, "friend-note.db");
    }
}
