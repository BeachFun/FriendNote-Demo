using TMPro;
using UnityEngine;

namespace RUI
{
    /// <summary>
    /// Фасад расширений (декораторов) TMP_InputField
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    [AddComponentMenu("RUI/TMP_InputField - Compositor")]
    public class InputField : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _placeholder;

        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_InputField_Validator _validator;
        [SerializeField] private TMP_InputField_BorderColor _borderColor;
        private ValidationRulesData _validationRules;

        public string Text
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }
        public bool TextIsValid
        {
            get => _validator is null ? true : _validator.IsContainValidData;
        }

        /// <summary>
        /// Правила валидации текста
        /// </summary>
        public ValidationRulesData ValidationRules
        {
            get => _validationRules;
            set
            {
                _validationRules = value;
                if (_validator is not null) _validator.ValidationRules = _validationRules;
            }
        }
        public TMP_InputField TargetInputField
        {
            get => _inputField;
        }

        // Unity UI Events API
        public TMP_InputField.SelectionEvent onSelect
        {
            get => _inputField.onSelect;
        }
        public TMP_InputField.SelectionEvent onDeselect
        {
            get => _inputField.onDeselect;
        }
        public TMP_InputField.OnChangeEvent onValueChanged
        {
            get => _inputField.onValueChanged;
        }
        public TMP_InputField.SubmitEvent onEndEdit
        {
            get => _inputField.onEndEdit;
        }


        private void OnValidate()
        {
            if (_inputField is null) _inputField = GetComponent<TMP_InputField>();
            if (_validator is null) _validator = GetComponent<TMP_InputField_Validator>();
            if (_borderColor is null) _borderColor = GetComponent<TMP_InputField_BorderColor>();

            if (_inputField is not null)
            {
                _inputField.placeholder.GetComponent<TMP_Text>().text = _placeholder;
            }
        }
    }
}
