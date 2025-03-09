using TMPro;
using UnityEngine;

namespace RUI
{
    [AddComponentMenu("RUI/TMP - FloatPlaceHolder")]

    [ExecuteAlways]
    public class TMP_FloatPlaceholder : MonoBehaviour
    {
        [SerializeField] private TMP_Text floatPlaceholder;

        private TMP_TypeEnum TMP_type = TMP_TypeEnum.None;
        private TMP_InputField inputField;
        private TMP_Dropdown dropdown;

        //private RectTransform rectTransformOriginal;
        //private RectTransform rectTransformSwap;

        [ExecuteAlways]
        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            dropdown = GetComponent<TMP_Dropdown>();

            if (inputField is not null)
            {
                TMP_type = TMP_TypeEnum.TMP_InputField;
                inputField.onValueChanged.AddListener(OnValueChanged);
            }

            if (dropdown is not null)
            {
                TMP_type = TMP_TypeEnum.TMP_DropdownField;
                dropdown.onValueChanged.AddListener(OnValueChanged);
            }

            if (TMP_type == TMP_TypeEnum.None)
            {
                Debug.LogError("Прикрепите один из полей ввода данных TextMeshPro library");
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            inputField = GetComponent<TMP_InputField>();
            dropdown = GetComponent<TMP_Dropdown>();

            if (inputField is not null)
            {
                TMP_type = TMP_TypeEnum.TMP_InputField;
                if (inputField != null) OnValueChanged(inputField.text);
            }

            if (dropdown is not null)
            {
                TMP_type = TMP_TypeEnum.TMP_DropdownField;
                if (dropdown != null) OnValueChanged(dropdown.value);
            }

            if (TMP_type == TMP_TypeEnum.None)
            {
                Debug.LogError("Прикрепите один из полей ввода данных TextMeshPro library");
            }
        }
#endif

        private void OnValueChanged(string text)
        {
            if (floatPlaceholder is null) return;

            if (string.IsNullOrEmpty(text))
            {
                floatPlaceholder.gameObject.SetActive(false);
            }
            else
            {
                floatPlaceholder.gameObject.SetActive(true);
            }
        }

        private void OnValueChanged(int i)
        {
            if (floatPlaceholder is null) return;

            if (-1 < i /*&& i < dropdown.options.Count*/) // unity значение за пределами массива считает последний элемент
            {
                floatPlaceholder.gameObject.SetActive(true);
            }
            else
            {
                floatPlaceholder.gameObject.SetActive(false);
            }
        }

        public enum TMP_TypeEnum
        {
            None,
            TMP_InputField,
            TMP_DropdownField
        }
    }
}
