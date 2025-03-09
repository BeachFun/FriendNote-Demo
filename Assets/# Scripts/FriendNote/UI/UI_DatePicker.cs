using System;
using Bitsplash.DatePicker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI
{
    /// <summary>
    /// this is a UI.Text implementation of the drop down
    /// </summary>
    public class UI_DatePicker : DatePickerDropDownBase
    {
        [Header("Настройка DatePickerDropDown")]
        [SerializeField] private RUI.InputField inputField;
        [Tooltip("Border settgins")]
        [SerializeField] private bool isHideBorder;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.cyan;
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;

        private Graphic targetGraphic;


        /// <summary>
        /// Введенная/выбранная дата в Datepicker. Возвращает null, если дата не вводилась или вводилась некорректно
        /// </summary>
        public DateTime? Date
        {
            get
            {
                try
                {
                    return string.IsNullOrEmpty(inputField.TargetInputField.text) ? null : Convert.ToDateTime(inputField.TargetInputField.text);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                inputField.Text = !value.HasValue || value.IsDefault()
                ? string.Empty
                : value.Value.ToString("dd.MM.yyyy");
                SetBorderColor(inputField.Text);
            }
        }
        public bool IsContainValidDate { get; private set; } = true;


        public TMP_InputField.SelectionEvent onSelect
        {
            get => inputField.onSelect;
        }
        public TMP_InputField.SelectionEvent onDeselect
        {
            get => inputField.onDeselect;
        }
        public TMP_InputField.OnChangeEvent onValueChanged
        {
            get => inputField.onValueChanged;
        }
        public TMP_InputField.SubmitEvent onEndEdit
        {
            get => inputField.onEndEdit;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isEditor && inputField is not null)
            {
                if (inputField is null) return;

                if (isHideBorder)
                {
                    inputField.TargetInputField.targetGraphic.enabled = false;
                }
                else
                {
                    inputField.TargetInputField.targetGraphic.enabled = true;
                    inputField.TargetInputField.targetGraphic.color = normalColor;
                }
            }
        }
#endif


        private void Awake()
        {
            targetGraphic = inputField.TargetInputField.targetGraphic;

            SwitchToNormal();
        }

        protected override void Start()
        {
            base.Start();

            onSelect.AddListener(Select);
            onEndEdit.AddListener(SetBorderColor);
            onDeselect.AddListener(SetBorderColor);
        }

        private void OnDestroy()
        {
            onSelect.RemoveListener(Select);
            onEndEdit.RemoveListener(SetBorderColor);
            onDeselect.RemoveListener(SetBorderColor);
        }

        protected override void SetText(string text)
        {
            if (inputField != null)
                inputField.Text = text;
        }


        private void Select(string text)
        {
            targetGraphic.enabled = true;
            targetGraphic.color = selectedColor;
        }

        private void SetBorderColor(string text)
        {
            targetGraphic.enabled = true;

            if (string.IsNullOrEmpty(inputField.Text))
            {
                SwitchToNormal();
            }
            else if (inputField.TextIsValid && Date is not null)
            {
                targetGraphic.color = validColor;
                SetText(Date?.ToString("dd.MM.yyyy"));
            }
            else
            {
                targetGraphic.color = invalidColor;
            }
        }

        private void SwitchToNormal()
        {
            if (isHideBorder)
            {
                targetGraphic.enabled = false;
            }
            else
            {
                targetGraphic.enabled = true;
                targetGraphic.color = normalColor;
            }
        }


        /// <summary>
        /// Нажатие на кнопку для открытия панели выбора даты
        /// </summary>
        public void OnSelectDate()
        {
            targetGraphic.enabled = true;
            targetGraphic.color = selectedColor;
        }

        /// <summary>
        /// Нажатие на кнопку для закрытия панели выбора даты
        /// </summary>
        public void OnDeselectDate()
        {
            if (Date is null)
            {
                SwitchToNormal();
            }
            else
            {
                targetGraphic.color = validColor;
            }
        }
    }
}
