using TMPro;
using UnityEngine;

namespace RUI
{
    [RequireComponent(typeof(TMP_InputField))]

    [ExecuteAlways]
    [AddComponentMenu("RUI/TMP_InputField - SizeFitter")]
    public class TMP_InputField_SizeFitter : MonoBehaviour
    {
        [SerializeField] private float placeholderPaddingTop;
        [SerializeField] private float placeholderPaddingBottom;

        // Получаем из Viewport компонента TMP_InputField
        private float viewportPaddingTop = 0f;
        private float viewportPaddingBottom = 0f;

        private TMP_InputField inputField;
        private RectTransform rectTransform;


        [ExecuteAlways]
        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            rectTransform = inputField.GetComponent<RectTransform>(); //inputField.textViewport;

            inputField.onValueChanged.AddListener(OnValueChanged);
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        private void Update()
        {
            inputField = GetComponent<TMP_InputField>();

            OnValueChanged(inputField.text);
        }
#endif

        [ExecuteAlways]
        private void OnDestroy()
        {
            if (inputField)
                inputField.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void OnValueChanged(string newText)
        {
            // Вычисляем установленные отступы текстового поля
            viewportPaddingTop = Mathf.Abs(inputField.textViewport.offsetMax.y);
            viewportPaddingBottom = inputField.textViewport.offsetMin.y;

            float preferredHeight = CalculatePreferredHeight();

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
        }


        private float CalculatePreferredHeight()
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                float pointSize = inputField.fontAsset is null ? inputField.pointSize : inputField.fontAsset.faceInfo.pointSize;
                return pointSize + placeholderPaddingTop + viewportPaddingTop + placeholderPaddingBottom + viewportPaddingBottom;
            }
            else
            {
                return inputField.preferredHeight;
            }
        }
    }
}
