using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FriendNote
{
    public class CyclicSelector : Selectable, IPointerClickHandler
    {
        [Header("CyclicSelector settings")]
        [SerializeField] private Sprite[] stateSprites;
        [SerializeField] private UnityEvent[] stateEvents;

        [Header("Debug")]
        [SerializeField] private int currentState = 0;

        //private Image 
        private bool lockClick;


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (currentState >= stateSprites.Length) return;
            image.sprite = stateSprites[currentState];

            if (currentState >= stateEvents.Length) return;
            stateEvents[currentState].Invoke();
        }
#endif


        protected override void Awake()
        {
            base.Awake();



            // Если размеры массивов данных не будут совпадать, то элемент будет заблокирован
            lockClick = stateEvents.Length != stateSprites.Length;
            if (lockClick)
            {
                Debug.LogError($"Кол-во элементов в {nameof(stateSprites)} и {nameof(stateEvents)} не совпадают друг с другом");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (lockClick) return;

            currentState = (currentState + 1) % stateEvents.Length; // переключение состояния

            image.sprite = stateSprites[currentState];
            stateEvents[currentState].Invoke();
        }
    }
}
