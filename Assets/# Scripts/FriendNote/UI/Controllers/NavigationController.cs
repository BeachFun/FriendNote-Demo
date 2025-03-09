using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace FriendNote.UI
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule inputSystem;

        private InputActionMap actionMap;
        private InputAction cancelAction;
        private bool isLockCloseAction;

        private void Awake()
        {
            if (inputSystem != null)
            {
                actionMap = inputSystem.actionsAsset.FindActionMap("UI");

                cancelAction = actionMap?.FindAction("Cancel");
                if (cancelAction is not null) cancelAction.performed += ClosePage;
            }
        }


        /// <summary>
        /// Метод обработчик, который принимает InputAction.CallbackContext
        /// </summary>
        private void ClosePage(InputAction.CallbackContext context)
        {
            if (isLockCloseAction) return;
            isLockCloseAction = true;

            StartCoroutine(LockCloseTimer()); // TODO: временное решение, пока не выясниться почему keyboard/escape вызывается дважды

            try
            {
                PagesController.Instance.CloseLastPage();
            }
            catch
            {
                Application.Quit();
            }
        }


        private IEnumerator LockCloseTimer()
        {
            yield return null; // пропуск одного кадра

            isLockCloseAction = false;
        }
    }
}
