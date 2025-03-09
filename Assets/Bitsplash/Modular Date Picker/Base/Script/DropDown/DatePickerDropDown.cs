using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bitsplash.DatePicker
{
    /// <summary>
    /// this is a UI.Text implementation of the drop down
    /// </summary>
    public class DatePickerDropDown : DatePickerDropDownBase
    {
        [Header("Настройка DatePickerDropDown")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Color borderSelectColor = Color.cyan;
        [SerializeField] private Color borderErrorColor = Color.red;
        [SerializeField] private Color borderCorrectColor = Color.green;


        private Image _border;


        public bool IsContainValidDate { get; private set; } = true;

        public DateTime Date
        {
            get => Convert.ToDateTime(inputField.text);
            set => /*SetText(*/value.Date.ToString("dd.MM.yyyy")/*)*/; // TODO: доработать
        }


        public TMP_InputField.SelectionEvent onSelect
        {
            get => inputField.onSelect;
            private set => inputField.onSelect = value;
        }
        public TMP_InputField.SelectionEvent onDeselect
        {
            get => inputField.onDeselect;
            private set => inputField.onDeselect = value;
        }
        public TMP_InputField.OnChangeEvent onValueChanged
        {
            get => inputField.onValueChanged;
            private set => inputField.onValueChanged = value;
        }
        public TMP_InputField.SubmitEvent onEndEdit
        {
            get => inputField.onEndEdit;
            private set => inputField.onEndEdit = value;
        }



        private void Awake()
        {
            _border = inputField.gameObject.GetComponent<Image>();
        }


        protected override void SetText(string text)
        {
            SetBorderColor(text);

            if (inputField != null)
                inputField.text = text;
        }

        public void ResetDate()
        {
            inputField.text = "";
        }

        private void SetBorderColor(string text)
        {
            // Пустое поле ввода не учитывается
            if (string.IsNullOrEmpty(inputField.text))
            {
                IsContainValidDate = true;
                _border.enabled = false;
                return;
            }

            try
            {
                if (Convert.ToDateTime(text) == DateTime.MinValue)
                {
                    IsContainValidDate = false;
                    _border.color = borderErrorColor;
                }
                else
                {
                    IsContainValidDate = true;
                    _border.color = borderCorrectColor;
                }
            }
            catch
            {
                IsContainValidDate = false;
                _border.color = borderErrorColor;
            }

            _border.enabled = true;
        }

        public void OnSelect()
        {
            _border.enabled = true;
            _border.color = borderSelectColor;
        }
    }
}
