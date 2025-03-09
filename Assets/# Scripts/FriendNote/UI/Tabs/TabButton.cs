using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FriendNote.UI
{
    public class TabButton : Selectable, IPointerClickHandler
    {
        [Header("TabButton settings")]
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text textCaption;

        [Header("Events Callbacks")]
        [Space]
        [SerializeField] private UnityEvent onTabClick;
        [SerializeField] private UnityEvent onTabUnactive;

        public Image Background => background;
        public TMP_Text Caption => textCaption;
        public UnityEvent OnTabUnactive => onTabUnactive;


        protected override void Awake()
        {
            base.Awake();

            if (background is null)
                background = GetComponent<Image>();

            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onTabClick?.Invoke();
            tabGroup.OnTabSelected(this);
        }
    }
}
