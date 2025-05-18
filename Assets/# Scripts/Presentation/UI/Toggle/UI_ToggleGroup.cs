using System.Collections.Generic;
using UnityEngine;

namespace FriendNote.UI
{
    public class UI_ToggleGroup : MonoBehaviour
    {
        [SerializeField] private List<UI_ToggleButton> toggleButtons; // кнопки переключения

        [SerializeField] private Color toggleIdle;
        [SerializeField] private Color togglePressed;

        [SerializeField] private UI_ToggleButton pressedButton;
        [SerializeField] private UI_ToggleButton prevPressedButton;


        public void Subscribe(UI_ToggleButton button)
        {
            if (toggleButtons == null)
            {
                toggleButtons = new List<UI_ToggleButton>();
            }

            button.Background.color = toggleIdle;

            toggleButtons.Add(button);
        }

        public void OnTabSelected(UI_ToggleButton button)
        {
            prevPressedButton = pressedButton;

            if (prevPressedButton == button)
                pressedButton = null;
            else
                pressedButton = button;

            ResetTabs();
        }

        private void ResetTabs()
        {
            foreach (UI_ToggleButton button in toggleButtons)
            {
                if (pressedButton != null && button == pressedButton)
                {
                    pressedButton.Background.color = togglePressed;
                    continue;
                }

                button.Background.color = toggleIdle;
            }
        }
    }
}
