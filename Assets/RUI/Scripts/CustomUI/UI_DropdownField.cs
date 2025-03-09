using System.Linq;
using TMPro;
using UnityEngine;

namespace RUI
{
    public class UI_DropdownField : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string dropdownPlaceholderText;
        [SerializeField] private string inputFieldPlaceholderText;

        [Header("References")]
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private InputField inputField;


        public bool IsContainValidData
        {
            get => inputField.TextIsValid;
        }
        public string Option
        {
            get => dropdown.options[dropdown.value].text;
            set => dropdown.value = value is null ? 0 : dropdown.options.Select(x => x.text).ToList().IndexOf(value);
        }
        public string Text
        {
            get => inputField.Text;
            set => inputField.Text = value;
        }

        /// <summary>
        /// Текст при пустом значении Dropdown
        /// </summary>
        public string DropdownPlaceholder
        {
            get => dropdownPlaceholderText;
            set
            {
                dropdownPlaceholderText = value;
                UpdatePlaceholders();
            }
        }

        /// <summary>
        /// Текст при пустом значении InputField
        /// </summary>
        public string InputFieldPlaceholder
        {
            get => inputFieldPlaceholderText;
            set
            {
                inputFieldPlaceholderText = value;
                UpdatePlaceholders();
            }
        }

        /// <summary>
        /// Правила валидации текста
        /// </summary>
        public ValidationRulesData ValidationRules
        {
            get => InputField.ValidationRules;
            set => InputField.ValidationRules = value;
        }

        public TMP_Dropdown TargetDropdown
        {
            get => dropdown;
        }
        public InputField InputField
        {
            get => inputField;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isEditor)
            {
                UpdatePlaceholders();
            }
        }
#endif

        private void UpdatePlaceholders()
        {
            if (dropdown is not null) dropdown.placeholder.GetComponent<TMP_Text>().text = dropdownPlaceholderText;
            if (inputField is not null) inputField.GetComponent<TMP_InputField>().placeholder.GetComponent<TMP_Text>().text = inputFieldPlaceholderText;
        }

        private void Awake()
        {
            if (dropdown is null)
            {
                Debug.LogError($"Отсутствует компонент '{typeof(TMP_Dropdown).Name}' в объекте и в дочерних объектах");
            }
            if (inputField is null)
            {
                Debug.LogError($"Отсутствует компонент '{typeof(InputField).Name}' в объекте и в дочерних объектах");
                return;
            }
        }


        /// <summary>
        /// Обновление значений выпадающего списка
        /// </summary>
        public void UpdateDropdownOptions(string[] options)
        {
            dropdown.options.Clear();

            foreach (string option in options)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(option));
            }
        }
    }
}
