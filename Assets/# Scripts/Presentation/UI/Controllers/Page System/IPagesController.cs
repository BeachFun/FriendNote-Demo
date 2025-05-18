using System;
using FriendNote.UI.Pages;
using UniRx;

namespace FriendNote.UI
{
    /// <summary>
    /// Контроллер динамических страниц
    /// </summary>
    public interface IPagesController
    {
        /// <summary>
        /// Словарь открытых страниц
        /// </summary>
        ReactiveCollection<Page> OpenedPages { get; }

        /// <summary>
        /// Открывает страницу Page
        /// </summary>
        Page OpenPage(string pageName);
        /// <summary>
        /// Открывает страницу InfoPage с некоторой информацией связанной с определенным человеком
        /// </summary>
        /// <param name="personId">ID человека для загрузки данных в InfoPage</param>
        PersonInfoPage OpenInfoPage(string pageName, int personId);
        /// <summary>
        /// Открывают страницу EditPage для редактирования данных
        /// </summary>
        /// <typeparam name="T">Тип данных, который будет передан странице</typeparam>
        /// <param name="data">Данные с которыми будет работать EditPage</param>
        EditPage<T> OpenEditPage<T>(string pageName, T data) where T : class, new();
        /// <summary>
        /// Закрывает указанную страницу, если она есть
        /// </summary>
        void ClosePage(Page page);
        /// <summary>
        /// Закрывает страницу по Guid (генерируется при создании страницы), если она есть
        /// </summary>
        void ClosePage(Guid pageId);
        /// <summary>
        /// Закрывает последнюю открытую страницу, если она есть
        /// </summary>
        void CloseLastPage();
    }
}
