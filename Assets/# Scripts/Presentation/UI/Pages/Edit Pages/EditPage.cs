using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FriendNote.UI.Pages
{
    /// <summary>
    /// Страница редактирования инфомрации
    /// </summary>
    /// <typeparam name="T">Тип данных с которым работает страница</typeparam>
    public abstract class EditPage<T> : Page, IDataUpdatable<T>
        where T : class, new()
    {
        [Header("Settings")]
        [SerializeField] protected EditModeEnum editMode;

        [Header("Actions")]
        [SerializeField] protected UnityEvent<Page> onPageUpdated;

        [Header("Required UI Page References")]
        [SerializeField] protected Button buttonCancel;
        [SerializeField] protected Button buttonSave1;
        [SerializeField] protected Button buttonSave2;
        [SerializeField] protected TMP_Text textCaption;

        protected readonly string _emptyField = "...";
        protected ReactiveProperty<T> _pageData = new();

        public IObservable<Page> UpdatedAsObservable => onPageUpdated.AsObservable().Select(_ => this);
        public EditModeEnum EditMode
        {
            get => editMode;
            set
            {
                editMode = value;
                SetCaption(value);
            }
        }


#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (textCaption is null) return;

            SetCaption(editMode);
        }
#endif

        protected virtual void Awake()
        {
            _pageData.Subscribe(x => Refresh()).AddTo(this);
        }

        protected override void Start()
        {
            ResetFields();

            base.Start();
        }

        /// <summary>
        /// Открытие страницы с обновлением данных страницы
        /// </summary>
        public virtual IOperable Open(T data)
        {
            UpdateData(data);
            return Open();
        }

        public virtual void UpdateData(T data)
        {
            onPageUpdated?.Invoke(this);

            if (data is not null)
            {
                _pageData.Value = data;
                EditMode = EditModeEnum.Updating;
            }
            else
            {
                EditMode = EditModeEnum.Adding;
            }
        }

        /// <summary>
        /// Сбор данных с полей страницы
        /// </summary>
        protected abstract T CollectData();

        /// <summary>
        /// Сохранение данных страницы
        /// </summary>
        public abstract void SaveData();

        /// <summary>
        /// Очищение полей страницы
        /// </summary>
        public abstract void ResetFields();

        /// <summary>
        /// Проверка корректного заполнения полей для ввода данных страницы
        /// </summary>
        /// <returns>true - good, false - bad</returns>
        protected abstract bool CheckFillValidation();


        /// <summary>
        /// Меняет заголовок страницы в зависимости от режима редактирования
        /// </summary>
        /// <param name="mode"></param>
        protected void SetCaption(EditModeEnum mode)
        {
            textCaption.text = mode switch
            {
                EditModeEnum.Adding => "Новая запись",
                EditModeEnum.Updating => "Изменить запись",
                _ => string.Empty
            };
        }


        protected void ShowSaveButtons()
        {
            bool isValid = CheckFillValidation();
            if (buttonSave1 is not null) buttonSave1.interactable = isValid;
            if (buttonSave2 is not null) buttonSave2.interactable = isValid;
        }
    }
}
