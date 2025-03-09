using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RUI
{
    [RequireComponent(typeof(TMP_InputField))]

    [AddComponentMenu("RUI/TMP_InputField - Validator")]
    public class TMP_InputField_Validator : MonoBehaviour
    {
        [Header("Text validation")]
        [SerializeField] protected bool isNotNullOrEmpty;
        [SerializeField] protected bool isAllCharacters = true;
        [SerializeField] protected UnicodeCategory[] validCharactersCategory;
        [Tooltip("Введите отдельный список допустимых символов")]
        [SerializeField] protected string otherSymbols;

        [Header("Settings")]
        [Tooltip("Проверка во время ввода текста")]
        [SerializeField] private bool isCheckedDuringEditing;


        public ValidatorStatesEnum State { get; private set; } = new();
        public bool IsContainValidData { get; protected set; } = true;
        public TMP_InputField TargetInputField { get; private set; } = null;
        public UnityEvent<ValidatorStatesEnum> onStateChanged { get; protected set; } = new();

        /// <summary>
        /// Правила валидации текста
        /// </summary>
        public ValidationRulesData ValidationRules
        {
            get => new()
            {
                IsNotNullOrEmpty = isNotNullOrEmpty,
                IsAllCharactersValid = isAllCharacters,
                ValidCharactersCategory = validCharactersCategory,
                ValidCharacters = otherSymbols
            };
            set
            {
                isNotNullOrEmpty = value.IsNotNullOrEmpty;
                isAllCharacters = value.IsAllCharactersValid;
                validCharactersCategory = value.ValidCharactersCategory;
                otherSymbols = value.ValidCharacters;
            }
        }

        private void Awake()
        {
            TargetInputField = GetComponent<TMP_InputField>();

            TargetInputField.onValueChanged.AddListener(OnValueChaged);
            TargetInputField.onEndEdit.AddListener(OnEndEdit);
        }

        public void Validate() => OnEndEdit(TargetInputField.text);

        private bool IsValid(string text)
        {
            if (string.IsNullOrEmpty(text) && isNotNullOrEmpty) return false;

            return isAllCharacters || text.All(IsCharacterValid);
        }

        private bool IsCharacterValid(char ch)
        {
            return validCharactersCategory.Contains(CharUnicodeInfo.GetUnicodeCategory(ch)) || otherSymbols.Contains(ch);
        }


        private void SetState(ValidatorStatesEnum state)
        {
            IsContainValidData = State != ValidatorStatesEnum.Invalid;
            State = state;
            onStateChanged.Invoke(state);
        }


        private void OnValueChaged(string str)
        {
            if (!IsValid(str))
            {
                SetState(ValidatorStatesEnum.Invalid);
            }
            else
            {
                SetState(ValidatorStatesEnum.None);
            }
        }

        private void OnEndEdit(string str)
        {
            if (string.IsNullOrEmpty(str) && !isNotNullOrEmpty)
            {
                SetState(ValidatorStatesEnum.None);
                return;
            }

            if (IsValid(str))
            {
                SetState(ValidatorStatesEnum.Valid);
            }
            else
            {
                SetState(ValidatorStatesEnum.Invalid);
            }
        }
    }
}
