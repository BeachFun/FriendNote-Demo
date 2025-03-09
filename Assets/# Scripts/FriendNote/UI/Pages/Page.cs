using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriendNote.UI.Pages
{
    public abstract class Page : MonoBehaviour, IOperable, IPageUpdatable
    {
        public UnityEvent<Page> PageOpened { get; protected set; } = new();
        public UnityEvent<Page> PageClosed { get; protected set; } = new();


        public Guid PageId { get; private set; } = Guid.NewGuid();


        protected virtual void OnEnable()
        {
            try
            {
                PageOpened.Invoke(this);
            }
            catch { }
        }

        protected virtual void OnDisable()
        {
            try
            {
                PageClosed.Invoke(this);
            }
            catch { }
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        public virtual IOperable Open()
        {
            this.gameObject.SetActive(true);
            return this;
        }

        /// <summary>
        /// Закрытие страницы
        /// </summary>
        public virtual void Close()
        {
            // this.gameObject.SetActive(false);
            DestroyImmediate(this.gameObject);
        }

        /// <summary>
        /// Обновление страницы
        /// </summary>
        public abstract void PageUpdate();
    }
}
