using System;
using System.Collections.Generic;
using System.Linq;
using FriendNote.Core.DTO;
using FriendNote.UI.Pages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace FriendNote.UI
{
    public class PagesController : MonoBehaviour, IController, IPagesController
    {
        public PagesController()
        {
            Instance = this;
        }

        [Header("Settings")]
        [Tooltip("Папка с префабами страниц, например: \"Pages/\".")]
        [SerializeField] private string pagesFolderPath = "Pages/";
        [SerializeField] private GameObject pagesContainer;

        [Header("Debug")]
        [SerializeField] protected List<Page> pageStack;

        [Header("Resource references | Info Pages")]
        [SerializeField] private string prefabPath_InfoPage_PersonProfile = "Page - Person Profile";
        [SerializeField] private string prefabPath_InfoPage_PersonResidences = "Page - Person Residences";
        [SerializeField] private string prefabPath_InfoPage_PersonEducation = "Page - Person Education";
        [SerializeField] private string prefabPath_InfoPage_PersonCareer = "Page - Person Career";
        [SerializeField] private string prefabPath_InfoPage_PersonSkills = "Page - Person Skills";
        [SerializeField] private string prefabPath_InfoPage_PersonInterests = "Page - Person Interests";
        [SerializeField] private string prefabPath_InfoPage_PersonGoals = "Page - Person Goals";
        [Header("EditPages prefabs | Edit Pages")]
        [SerializeField] private string prefabPath_EditPage_PersonProfile = "Edit Page - Person Profile";
        [SerializeField] private string prefabPath_EditPage_PersonResidences = "Edit Page - Person Residence";
        [SerializeField] private string prefabPath_EditPage_PersonEducation = "Edit Page - Person Education";
        [SerializeField] private string prefabPath_EditPage_PersonWorkPosition = "Edit Page - Person Work Position";
        [SerializeField] private string prefabPath_EditPage_PersonSkills = "Edit Page - Person Skill";
        [SerializeField] private string prefabPath_EditPage_PersonInterests = "Edit Page - Person Interest";
        [SerializeField] private string prefabPath_EditPage_PersonGoals = "Edit Page - Person Goal";

        public static PagesController Instance { get; private set; }


        public ServiceStatus Status { get; }
        public List<Page> OpenedPages { get => pageStack; private set => pageStack = value; }


        /// <summary>
        /// Открытие неизвестных скрипту страниц
        /// </summary>
        /// <param name="pageName">Название префаба</param>
        public T OpenPage<T>(string pageName) where T : Page
        {
            if (string.IsNullOrEmpty(pageName))
            {
                Debug.LogWarning("Передано пустое название страницы");
                return null;
            }

            // Загружаем префаб страницы
            var pagePrefab = Resources.Load<T>(pagesFolderPath + pageName);
            if (pagePrefab == null)
            {
                Debug.LogWarning($"Не найден префаб страницы с именем '{pageName}' в папке '{pagesFolderPath}' или " +
                    $"префаб страницы '{pageName}' не содержит компонент Page.");
                return null;
            }

            // Создаем копию префаба на сцене
            var instantiatedPage = Instantiate(pagePrefab, pagesContainer.transform).GetComponent<T>();
            instantiatedPage.Open();

            AddPageToStack(instantiatedPage);
            Debug.Log($"Page '{typeof(T).Name}' is opened");

            return instantiatedPage as T;
        }

        /// <summary>
        /// Открытие известной скрипту InfoPage
        /// </summary>
        /// <typeparam name="T">Структура данных с которой работает InfoPage для определения нужной страницы для октрытия</typeparam>
        /// <param name="id">ID записи для представления в InfoPage</param>
        public InfoPage OpenInfoPage<T>(int id) where T : class, new()
        {
            var page = OpenPage<InfoPage>(this.GetInfoPagePath<T>()) as InfoPage;

            if (page is null)
            {
                Debug.LogWarning("Нет подходящей страницы для представления переданной структуры данных");
                return null;
            }

            page.DataUpdate(id);

            return page;
        }


        // for Unity callback API
        public void OpenEditPagePersonInfo()
        {
            OpenEditPage<PersonProfile>(null);
        }


        public EditPage<T> OpenEditPage<T>(T data) where T : class, new()
        {
            try
            {
                var page = OpenPage<EditPage<T>>(this.GetEditPagePath<T>()) as EditPage<T>;

                if (page is null)
                    throw new Exception("Нет подходящей страницы для редактирования переданных данных");

                page.DataUpdate(data);

                AddPageToStack(page);

                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }

        /// <summary>
        /// Открытие страницы для добавления связанных с человеком данных
        /// </summary>
        public RelatedInfoEditPage<T> OpenRelatedInfoEditPage<T>(int personId) where T : EntityBase, IPersonRelatedInfo, new()
        {
            try
            {
                if (personId < 0)
                    throw new Exception($"Передан некорретный petonsId: {personId}");

                var page = OpenPage<RelatedInfoEditPage<T>>(this.GetEditPagePath<T>()) as RelatedInfoEditPage<T>;

                if (page is null)
                    throw new Exception("Нет подходящей страницы для редактирования переданных данных");

                page.PersonId = personId;

                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }

        /// <summary>
        /// Открытие страницы для редактирования связанных с человеком данных
        /// </summary>
        public RelatedInfoEditPage<T> OpenRelatedInfoEditPage<T>(T data) where T : EntityBase, IPersonRelatedInfo, new()
        {
            try
            {
                // Валидация входных данных
                if (data is null)
                    throw new Exception("Передан объект со значением null");
                bool? isEntityExist = Services.EntityData.ExistEntity<T>(data.Id.Value);
                if (!isEntityExist.HasValue || !isEntityExist.Value)
                    throw new Exception($"Записи с ID {data.Id} не существует чтобы отредактировать в EditPage");


                // Основная часть алгоритма
                var page = OpenPage<RelatedInfoEditPage<T>>(this.GetEditPagePath<T>()) as RelatedInfoEditPage<T>;

                if (page is null)
                {
                    Debug.LogWarning("Нет подходящей страницы для редактирования переданных данных");
                    return null;
                }

                page.DataUpdate(data);
                page.PersonId = data.PersonId.Value; // personId гарантировано есть и актуален

                return page;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                return null;
            }
        }


        /// <summary>
        /// Закрывает указанную страницу, если она есть
        /// </summary>
        public void ClosePage(Page page)
        {
            if (!OpenedPages.Contains(page)) return;

            OpenedPages[OpenedPages.IndexOf(page)].Close();
            OpenedPages.Remove(page);
        }

        /// <summary>
        /// Закрывает страницу по Guid, если она есть
        /// </summary>
        public void ClosePage(Guid pageId)
        {
            if (!OpenedPages.Any(x => x.PageId == pageId)) return;

            int index = OpenedPages.IndexOf(OpenedPages.FirstOrDefault(x => x.PageId == pageId));

            OpenedPages[index].Close();
            OpenedPages.RemoveAt(index);
        }

        /// <summary>
        /// Закрывает последнюю открытую страницу, если такая есть 
        /// </summary>
        /// <exception cref="NullReferenceException">Выбрасывает исключение в случае отсутствия страницы</exception>
        public void CloseLastPage()
        {
            OpenedPages.Last().Close();
        }


        /// <summary>
        /// Фабричный метод для получения названия InfoPage для загрузки из ресурсов
        /// </summary>
        private string GetInfoPagePath<T>() where T : class, new()
        {
            if (typeof(T) == typeof(PersonProfile)) return prefabPath_InfoPage_PersonProfile;
            if (typeof(T) == typeof(Residence)) return prefabPath_InfoPage_PersonResidences;
            if (typeof(T) == typeof(Education)) return prefabPath_InfoPage_PersonEducation;
            if (typeof(T) == typeof(WorkPosition)) return prefabPath_InfoPage_PersonCareer;
            if (typeof(T) == typeof(Skill)) return prefabPath_InfoPage_PersonSkills;
            if (typeof(T) == typeof(Interest)) return prefabPath_InfoPage_PersonInterests;
            if (typeof(T) == typeof(Goal)) return prefabPath_InfoPage_PersonGoals;

            Debug.LogWarning("Передан неизвестный тип данных");

            return null;
        }

        /// <summary>
        /// Фабричный метод для получения названия EditPage для загрузки из ресурсов
        /// </summary>
        private string GetEditPagePath<T>() where T : class, new()
        {
            if (typeof(T) == typeof(PersonProfile)) return prefabPath_EditPage_PersonProfile;
            if (typeof(T) == typeof(Residence)) return prefabPath_EditPage_PersonResidences;
            if (typeof(T) == typeof(Education)) return prefabPath_EditPage_PersonEducation;
            if (typeof(T) == typeof(WorkPosition)) return prefabPath_EditPage_PersonWorkPosition;
            if (typeof(T) == typeof(Skill)) return prefabPath_EditPage_PersonSkills;
            if (typeof(T) == typeof(Interest)) return prefabPath_EditPage_PersonInterests;
            if (typeof(T) == typeof(Goal)) return prefabPath_EditPage_PersonGoals;

            Debug.LogWarning("Передан неизвестный тип данных");

            return null;
        }


        /// <summary>
        /// Добавляет страницу в стек открытых страниц
        /// </summary>
        protected void AddPageToStack(Page page)
        {
            OpenedPages.Add(page);
            page.PageClosed.AddListener(OnPageClosed);
        }
        private void OnPageClosed(Page page)
        {
            OpenedPages.Remove(page);
            Debug.Log($"Page '{page.name}' is closed");
            OpenedPages.Last()?.PageUpdate();
        }
    }
}
