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



            // ���� ������� �������� ������ �� ����� ���������, �� ������� ����� ������������
            lockClick = stateEvents.Length != stateSprites.Length;
            if (lockClick)
            {
                Debug.LogError($"���-�� ��������� � {nameof(stateSprites)} � {nameof(stateEvents)} �� ��������� ���� � ������");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (lockClick) return;

            currentState = (currentState + 1) % stateEvents.Length; // ������������ ���������

            image.sprite = stateSprites[currentState];
            stateEvents[currentState].Invoke();
        }
    }
}
