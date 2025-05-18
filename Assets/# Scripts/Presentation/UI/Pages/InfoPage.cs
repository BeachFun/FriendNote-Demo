using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace FriendNote.UI.Pages
{
    public abstract class InfoPage<T> : Page, IDataUpdatable<T>
        where T : new()
    {
        [Header("Actions")]
        [SerializeField] protected UnityEvent<Page> onPageUpdated;

        protected readonly string _emptyField = "...";
        protected ReactiveProperty<T> _pageData = new();

        public IObservable<Page> UpdatedAsObservable => onPageUpdated.AsObservable().Select(_ => this);


        protected virtual void Awake()
        {
            _pageData.Subscribe(x => Refresh()).AddTo(this);
        }

        /// <summary>
        /// Открытие страницы с обновлением данных страницы
        /// </summary>
        public virtual IOperable Open(T data)
        {
            UpdateData(data);
            return Open();
        }

        /// <summary>
        /// Обновление данных страницы
        /// </summary>
        public virtual void UpdateData(T data)
        {
            onPageUpdated?.Invoke(this);

            _pageData.Value = data;
        }
    }
}
