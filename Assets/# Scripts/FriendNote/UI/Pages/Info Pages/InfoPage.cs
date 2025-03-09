using UnityEngine.Events;

namespace FriendNote.UI.Pages
{
    /// <summary>
    /// Информативная страница с автозагрузкой данных
    /// </summary>
    public abstract class InfoPage : Page
    {
        protected string _emptyField = "...";

        public UnityEvent<Page> onPageUpdated { get; protected set; } = new();


        /// <summary>
        /// Открытие страницы с обновлением данных страницы
        /// </summary>
        public virtual IOperable Open(int id)
        {
            DataUpdate(id);
            return Open();
        }

        /// <summary>
        /// Обновление данных страницы
        /// </summary>
        public virtual void DataUpdate(int id)
        {
            onPageUpdated?.Invoke(this);
        }
    }
}
