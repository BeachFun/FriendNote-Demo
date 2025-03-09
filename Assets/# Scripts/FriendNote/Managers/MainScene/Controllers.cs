using UnityEngine;

namespace FriendNote.UI
{
    [RequireComponent(typeof(PagesController))]

    /// <summary>
    /// Управляющий/контроллер всеми диспетчерами сцены MainScene.
    /// </summary>
    public class Controllers : MonoBehaviour
    {
        public static PagesController Pages { get; private set; }


        private void Awake()
        {
            // Инициализация менеджеров
            Pages = GetComponent<PagesController>();
        }

        private void OnDestroy()
        {
            Pages = null;
        }
    }
}
