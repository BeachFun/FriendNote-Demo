using System;
using System.Linq;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using FriendNote.UI.Pages;
using UniRx;
using UnityEngine;
using Zenject;

namespace FriendNote.UI
{
    /// <summary>
    /// Предоставляет средства для работы со страницами. Открытие, закрытие, просмотр открытых страниц.
    /// Открытие и закрытие страниц происходит путем создания и удаления объекта на сцене.
    /// </summary>
    public class PagesController : MonoBehaviour, IPagesController
    {
        [Header("Bindings")]
        [SerializeField] private GameObject pagesContainer;
        [Header("References")]
        [SerializeField] private Core.KeyValuePair<string, Page>[] _pages;

        [Inject] private DiContainer _container;

        public ReactiveCollection<Page> OpenedPages { get; } = new();


        private void Start()
        {
            print("Startup: PageController is started");
        }

        public Page OpenPage(string pageName)
        {
            try
            {
                if (string.IsNullOrEmpty(pageName))
                    throw new Exception("Передано пустое название страницы");

                var pair = _pages.FirstOrDefault(x => x.Key == pageName);

                if (pair is null)
                    throw new Exception($"Страницы с названием '{pageName}' нет в списке страниц");

                // Создаем копию префаба на сцене
                var page = Instantiate(pair.Value, pagesContainer.transform).GetComponent<Page>();
                _container.Inject(page);
                page.Open();

                // Внесение в список и логирование
                OpenedPages.Add(page);
                page.ClosedAsObservable.Subscribe(OnPageClosed).AddTo(this);

                Debug.Log($"Page '{pageName}' is opened");
                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }

        public PersonInfoPage OpenInfoPage(string pageName, int personId)
        {
            try
            {
                var page = OpenPage(pageName) as PersonInfoPage;

                if (page is null)
                    throw new Exception("Нет подходящей страницы для представления переданной структуры данных");

                page.UpdateData(personId);

                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }

        public EditPage<T> OpenEditPage<T>(string pageName, T data) where T : class, new()
        {
            try
            {
                var page = OpenPage(pageName) as EditPage<T>;

                if (page is null)
                    throw new Exception("Нет подходящей страницы для редактирования переданных данных");

                page.UpdateData(data);

                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }

        public void ClosePage(Page page)
        {
            try
            {
                if (!OpenedPages.Contains(page))
                    throw new Exception("Нельзя закрыть странцу. Страницы нет в списке открытых страниц.");

                OpenedPages[OpenedPages.IndexOf(page)].Close();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }

        public void ClosePage(Guid pageId)
        {
            try
            {
                if (!OpenedPages.Any(x => x.PageId == pageId))
                    throw new Exception($"Нельзя закрыть странцу. Страницы с GUID {pageId} нет в списке откртых страниц.");

                int index = OpenedPages.IndexOf(OpenedPages.FirstOrDefault(x => x.PageId == pageId));

                OpenedPages[index].Close();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }

        public void CloseLastPage()
        {
            OpenedPages.Last()?.Close();
        }


        private void OnPageClosed(Page page)
        {
            Debug.Log($"Page '{page.name}' is closed");

#if UNITY_EDITOR
            DestroyImmediate(page.gameObject);
#else
            Destroy(page.gameObject);
#endif

            OpenedPages.Remove(page);
            OpenedPages.LastOrDefault()?.Refresh();
        }


        // Unity API
        public void OpenAddPersonPage()
        {
            // TODO: убрать, сделать страницу EditPersonProfile для добавления нового знакомого статичной
            OpenEditPage<PersonProfile>(PageRegistry.GetEditPageId<PersonProfile>(), null);
        }
    }
}
