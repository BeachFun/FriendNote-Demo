using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FriendNote.UI
{
    public class UI_ToggleButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UI_ToggleGroup toggleGroup;
        [SerializeField] private Image background;

        private Button _button;

        public Image Background => background;

        private void Awake()
        {
            if (background is not null)
                background = GetComponent<Image>();

            _button = GetComponent<Button>();

            if (toggleGroup is not null)
                toggleGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();

            if (toggleGroup is not null)
                toggleGroup.OnTabSelected(this);
        }

        public virtual void OnClick()
        {

        }
    }
}
