using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace FriendNote.UI
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule inputSystem;

        [Inject] private IPagesController pagesController;

        private bool isLockCloseAction;
        private InputActionMap actionMap;
        private InputAction cancelAction;

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

            StartCoroutine(LockCloseTimer()); // TODO: Удалить. Временное решение, пока не выясниться почему keyboard/escape вызывается дважды

            try
            {
                pagesController.CloseLastPage();
            }
            catch
            {
                Application.Quit();
            }
        }


        private IEnumerator LockCloseTimer() // TODO: Удалить
        {
            yield return null; // пропуск одного кадра

            isLockCloseAction = false;
        }
    }
}
