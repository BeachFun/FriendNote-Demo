using FriendNote.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace FriendNote.UI
{
    public class StartupController : MonoBehaviour
    {
        [Tooltip("����� ������� ���������� ����� ������� ���� ��������")]
        [SerializeField] private string sceneName;

        [Inject] private IDataService dataService;

        private void Awake()
        {
            dataService.Status
                .Where(x => x == Core.ServiceStatus.Started)
                .Take(1)
                .Subscribe(x => LoadScene());
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
