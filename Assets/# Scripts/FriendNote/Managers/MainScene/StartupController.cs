using Observer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FriendNote.UI
{
    public class StartupController : MonoBehaviour
    {
        [Tooltip("Сцена которая запустится после запуска всех сервисов")]
        [SerializeField] private string sceneName;

        private void Awake()
        {
            Services.Initialize();
            Messenger.AddListener(Notices.StartupNotice.ALL_SERVICES_STARTED, LoadScene);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(Notices.StartupNotice.ALL_SERVICES_STARTED, LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
