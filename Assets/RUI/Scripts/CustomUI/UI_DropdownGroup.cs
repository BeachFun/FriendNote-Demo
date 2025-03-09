using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RUI
{
    /// <summary>
    /// UI представляющий группу UI_DropdownField с динамическим добавлением и удалением лишних полей (всегда остается одно пустое поле)
    /// </summary>
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class UI_DropdownGroup : MonoBehaviour
    {
        [Header("DropdownGroup settings")]
        [SerializeField] private string[] dropdownOptions; // все dropdown имеют эти значения
        [SerializeField] private string placeholderText;

        [Header("InputField validation")]
        [SerializeField] private bool isNotNullOrEmpty;
        [SerializeField] private bool isAllCharacters = true;
        [SerializeField] private UnicodeCategory[] validCharactersCategory;
        [Tooltip("Введите отдельный список допустимых символов")]
        [SerializeField] private string validCharacters; // TODO: добавить атрибут для скрытия в инспекторе если isStandartCharacters == true

        [Space(10), Header("Deliter settings")]
        [Tooltip("Разделитель между DropdownFields")]
        [SerializeField] private string deliterTag = "deliter";
        [SerializeField] private bool isDeliterRenderer = true;

        [Space(10), Header("Events callbacks")]
        [Space(5), SerializeField] private TMP_InputField.SelectionEvent onSelect = new();
        [Space(5), SerializeField] private TMP_InputField.SelectionEvent onDeselect = new();
        [Space(5), SerializeField] private TMP_InputField.OnChangeEvent onValueChanged = new();
        [Space(5), SerializeField] private TMP_InputField.SubmitEvent onEndEdit = new();

        //[Header("DropdownGroup references")]
        //[SerializeField] private UI_BorderSelected borderSelected;

        [Header("Prefab references")]
        [SerializeField] private UI_DropdownField dropdownFieldPrefab;
        [SerializeField] private GameObject deliterPrefab;


        private List<UI_DropdownField> _dropdownList = new();


        /// <summary>
        /// Item1 - значение dropdown, Item2 - значения inputField
        /// </summary>
        public (string, string)[] InputFieldsData
        {
            get => _dropdownList.Select(x => (x.Option, x.Text)).Take(_dropdownList.Count - 1).ToArray(); // считывает все кроме последнего пустого
            set
            {
                DestroyAllChilds();
                foreach (var data in value)
                {
                    AddDropdownField(data.Item1, data.Item2);
                }
                AddDropdownField();
            }
        }
        /// <summary>
        /// В подчиненных DropdownFields введенны корректные данные
        /// </summary>
        public bool IsContainValidData { get; private set; } = true;


        #region Unity event API

        /// <summary>
        /// Вызывается после получении фокуса или при выборе пункта из выпадающего списка любого дочернего поля для ввода
        /// </summary>
        public TMP_InputField.SelectionEvent OnSelect
        {
            get => onSelect;
            set => onSelect = value;
        }
        /// <summary>
        /// Вызывается после потере фокуса на любом дочернем поле для ввода
        /// </summary>
        public TMP_InputField.SelectionEvent OnDeselect
        {
            get => onDeselect;
            set => onDeselect = value;
        }
        /// <summary>
        /// Вызывается после изменении данных в любом из дочерних полей для ввода или при изменении значения любого дочернего Dropdown
        /// </summary>
        public TMP_InputField.OnChangeEvent OnValueChanged
        {
            get => onValueChanged;
            set => onValueChanged = value;
        }
        /// <summary>
        /// Вызывается после прекращения изменения данных в любом из полей для ввода ДО потери фокуса
        /// </summary>
        public TMP_InputField.SubmitEvent OnEndEdit
        {
            get => onEndEdit;
            set => onEndEdit = value;
        }

        #endregion


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isEditor)
            {
                var dropdownList = this.GetComponentsInChildren<UI_DropdownField>().ToList();

                dropdownList.ForEach(x =>
                {
                    x.UpdateDropdownOptions(dropdownOptions);
                    x.InputFieldPlaceholder = placeholderText;
                });
            }
        }
#endif


        private void Awake()
        {
            //if (borderSelected is null) borderSelected = GetComponent<UI_BorderSelected>();

            Clear();
        }

        #region [Методы] Взаимодействие с дочерними DropdownFields

        /// <summary>
        /// Создание дочернего DropdownField в DropdownGroup. Пустой по-умолчанию 
        /// </summary>
        public UI_DropdownField AddDropdownField(string option = null, string text = null)
        {
            if (dropdownFieldPrefab is null) return null;

            // Инициализация объекта и разделителя на сцене
            if (_dropdownList.Count > 0) Instantiate(deliterPrefab, this.transform);
            GameObject dropdownFieldGO = Instantiate(dropdownFieldPrefab.gameObject, this.transform)
                ?? throw new Exception("Не удалось создать DropdownField");

            // Настройка созданного DropdownField
            var dropdownField = dropdownFieldGO.GetComponent<UI_DropdownField>();
            {
                // Настройка опций в Dropdown
                dropdownField.UpdateDropdownOptions(dropdownOptions);

                // Настройка значений DropdownField
                dropdownField.Text = text;
                dropdownField.Option = option;

                // Настройка валидации текста
                dropdownField.ValidationRules = new ValidationRulesData()
                {
                    IsNotNullOrEmpty = isNotNullOrEmpty,
                    IsAllCharactersValid = isAllCharacters,
                    ValidCharactersCategory = validCharactersCategory,
                    ValidCharacters = validCharacters
                };

                // Настройка ссылок
                //dropdownField.BorderSelected = borderSelected;

                // Связывание стандартных событий
                dropdownField.InputField.onSelect.AddListener((text) => OnSelect.Invoke(text));
                dropdownField.InputField.onEndEdit.AddListener((text) => OnEndEdit.Invoke(text));
                dropdownField.InputField.onDeselect.AddListener((text) => OnDeselect.Invoke(text));
                dropdownField.InputField.onValueChanged.AddListener((text) => OnValueChanged.Invoke(text));
                dropdownField.InputField.onEndEdit.AddListener(OnInputFieldValueEndEdit);
            }

            // Изменение placeholder'а
            if (!string.IsNullOrEmpty(placeholderText))
            {
                TMP_Text placeholder = dropdownField.InputField.TargetInputField.placeholder.GetComponent<TMP_Text>();
                if (placeholder is not null) placeholder.text = placeholderText;
            }

            _dropdownList.Add(dropdownField);

            // После добавления дочернего объекта принудительно обновляем layout, чтобы визуально ничего не дергалось
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

            return dropdownField;
        }

        /// <summary>
        /// Удаление дочернего DropdownField
        /// </summary>
        public void RemoveDropdownField(int fieldIndex)
        {
            // Удаление InputField
            if (_dropdownList is null || fieldIndex >= _dropdownList.Count) return;
            DestroyImmediate(_dropdownList[fieldIndex].gameObject); // удаление со сцены
            _dropdownList.RemoveAt(fieldIndex); // удаление со списка

            // Удаление Deliter
            var deliters = this.transform.GetComponentsInChildren<Transform>()
                .Where(x => x.gameObject.tag == deliterTag)
                .ToArray();
            if (deliters is not null && deliters.Length > fieldIndex)
            {
                DestroyImmediate(deliters[fieldIndex].gameObject);
            }

            // После удаления дочерних объектов принудительно обновляем layout, чтобы визуально ничего не дергалось
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        /// <summary>
        /// Изменение значения текста дочернего InputField
        /// </summary>
        public void SetDropdownField(int fieldIndex, string text, string option)
        {
            if (_dropdownList is null || fieldIndex >= _dropdownList.Count) return;

            _dropdownList[fieldIndex].Text = text;
            _dropdownList[fieldIndex].Option = option;
        }

        /// <summary>
        /// Удаляет все UI_DropdownField и создает один пустой
        /// </summary>
        public void Clear()
        {
            DestroyAllChilds();
            AddDropdownField(); // должен быть хотя бы 1 пустой DropdownField
        }

        /// <summary>
        /// Удаление всех дочерних UI_DropdownField
        /// </summary>
        private void DestroyAllChilds()
        {
            // Удаление всех DropdownFields
            var dropdownFields = this.transform.GetComponentsInChildren<UI_DropdownField>();
            foreach (var dropdownField in dropdownFields)
            {
                DestroyImmediate(dropdownField.gameObject);
            }
            _dropdownList.Clear();

            // Удаление всех разделителей между DropdownFields
            var deliters = this.transform.GetComponentsInChildren<Transform>()
                .Where(x => x.gameObject.tag == deliterTag)
                .ToArray();
            foreach (var deliter in deliters)
            {
                DestroyImmediate(deliter.gameObject);
            }

            // После удаления дочерних объектов принудительно обновляем layout, чтобы визуально ничего не дергалось
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        #endregion

        protected virtual void OnInputFieldValueEndEdit(string text)
        {
            // Алгоритм для создания новых полей после введения текста, если нет пустых полей для ввода текста

            var inputFields = this.transform.GetComponentsInChildren<InputField>();

            // Создание нового пустого поля, если заполнялось единственное пустое поле
            if (inputFields.Select(x => x.Text).All(x => !string.IsNullOrEmpty(x)))
            {
                AddDropdownField(null, null);
                inputFields = this.transform.GetComponentsInChildren<InputField>();
            }

            // Удаление всех пустых полей с конца, если их больше чем 1
            for (int i = 0; i < inputFields.Length; i++)
            {
                if (string.IsNullOrEmpty(inputFields[i].Text) &&
                    inputFields.Select(x => x.Text).Count(x => string.IsNullOrEmpty(x)) > 1)
                {
                    RemoveDropdownField(i);
                    Debug.Log($"удалено поле с индексом '{i}'");
                    inputFields = this.transform.GetComponentsInChildren<InputField>();
                    i--;
                }
            }
        }
    }
}
