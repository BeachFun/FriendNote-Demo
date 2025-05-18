using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace FriendNote.UI.Pages
{
    public abstract class Page : MonoBehaviour, IOperable, IRefreshable
    {
        [Header("Actions")]
        [SerializeField] protected UnityEvent<Page> onOpened;
        [SerializeField] protected UnityEvent<Page> onClosed;

        public Guid PageId { get; private set; } = Guid.NewGuid();
        public IObservable<Page> OpenedAsObservable => onOpened.AsObservable().Select(_ => this);
        public IObservable<Page> ClosedAsObservable => onClosed.AsObservable().Select(_ => this);


        protected virtual void Start()
        {
            Refresh();
        }

        /// <summary>
        /// Открывает страницу
        /// </summary>
        public virtual IOperable Open()
        {
            this.gameObject.SetActive(true);
            onOpened.Invoke(this);
            return this;
        }

        /// <summary>
        /// Закрывает страницу
        /// </summary>
        public virtual void Close()
        {
            this.gameObject.SetActive(false);
            onClosed.Invoke(this);
        }

        /// <summary>
        /// Обновляет страницу
        /// </summary>
        public abstract void Refresh();
    }
}
