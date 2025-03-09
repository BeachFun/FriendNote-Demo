using System;
using System.Collections.Generic;
using FriendNote.UI.Pages;

namespace FriendNote.UI
{
    /// <summary>
    /// Контроллер динамических страниц
    /// </summary>
    public interface IPagesController
    {
        /// <summary>
        /// Словарь открытых страниц, где строка название
        /// </summary>
        List<Page> OpenedPages { get; }

        T OpenPage<T>(string pageName) where T : Page;
        void ClosePage(Page page);
        void ClosePage(Guid pageId);
    }
}
