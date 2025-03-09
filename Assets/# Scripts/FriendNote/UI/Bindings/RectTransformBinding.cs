using UnityEngine;

namespace FriendNote
{
    [RequireComponent(typeof(RectTransform))]

    [AddComponentMenu("UI/Binding/RectTransformBinding")]

    [ExecuteAlways]
    public class RectTransformBinding : MonoBehaviour
    {
        [SerializeField] private RectTransform sourceRectTransform;

        [Space(7), Header("Settings")]

        [Space(3)]
        [SerializeField] private bool isWidth;
        [SerializeField, Range(.1f, 5f)] private float widthMultiplier = 1f;

        [Space(3)]
        [SerializeField] private bool isHeight;
        [SerializeField, Range(.1f, 5f)] private float heightMultiplier = 1f;

        [Space(3)]
        [SerializeField] private bool isAnchors;


        private RectTransform targetRectTransform;


        [ExecuteAlways]
        public void Awake()
        {
            targetRectTransform = GetComponent<RectTransform>();

            CopyValues();
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        public void Update()
        {
            CopyValues();
        }
#endif


        private void CopyValues()
        {
            if (sourceRectTransform is null) return;

            // �������� �������
            //targetRectTransform.anchoredPosition = sourceRectTransform.anchoredPosition;

            // �������� �������
            if (isWidth || isHeight)
            {
                Vector2 sizeDelta = sourceRectTransform.sizeDelta;
                if (isWidth) sizeDelta.x *= widthMultiplier;
                if (isHeight) sizeDelta.y *= heightMultiplier;
                targetRectTransform.sizeDelta = sizeDelta;
            }

            // �������� anchorMin � anchorMax
            if (isAnchors)
            {
                targetRectTransform.anchorMin = sourceRectTransform.anchorMin;
                targetRectTransform.anchorMax = sourceRectTransform.anchorMax;
            }

            // �������� pivot
            //targetRectTransform.pivot = sourceRectTransform.pivot;

            // �������� �������
            //targetRectTransform.localEulerAngles = sourceRectTransform.localEulerAngles;

            // �������� �������
            //targetRectTransform.localScale = sourceRectTransform.localScale;
        }
    }
}
