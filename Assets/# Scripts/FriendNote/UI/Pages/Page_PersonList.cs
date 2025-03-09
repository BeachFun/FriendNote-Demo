using System.Collections.Generic;
using FriendNote.Core.DTO;
using Observer;
using TMPro;
using UnityEngine;
using UnlimitedScrollUI;

namespace FriendNote.UI.Pages
{
    [ExecuteAlways]
    public class Page_PersonList : Page
    {
        [Header("References")]
        [SerializeField] private GameObject panelSearch;
        [SerializeField] private TMP_InputField inputFieldSearch;
        [Tooltip("Объект в котором будут создаваться записи")]
        [SerializeField] private VerticalUnlimitedScroller verticalLayout;

        [Header("Prefab References")]
        [SerializeField] private GameObject _listelementPerson;

        private List<PersonBasicInfo> persons = new();


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            if (!Application.isPlaying) return;

            // Удаление всех тестовых элементов списка
            UnityHelper.DestroyAllChildren(verticalLayout.gameObject);

            Messenger.AddListener(Notices.DataNotice.PERSONS_DATA_UPDATED, PageUpdate);
            Messenger.AddListener(Notices.UINotice.INFO_PAGE_PERSON_PROFILE_CLOSED, PageUpdate);
            Messenger.AddListener(Notices.UINotice.EDIT_PAGE_PERSON_PROFILE_CLOSED, PageUpdate);
            PageOpened.AddListener(PageUpdate);
        }

        private void Start()
        {
            // Настройка размеров
            var rectSource = panelSearch.GetComponent<RectTransform>();
            var rectTarget = verticalLayout.GetComponent<RectTransform>();
            Vector2 sizeDelta = rectTarget.sizeDelta;
            sizeDelta.x = rectSource.sizeDelta.x;
            rectTarget.sizeDelta = sizeDelta;
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        private void Update()
        {
            UnlimitedScrollerSetSize();
        }
#endif

        private void OnDestroy()
        {
            if (!Application.isPlaying) return;

            try
            {
                Messenger.RemoveListener(Notices.DataNotice.PERSONS_DATA_UPDATED, PageUpdate);
                Messenger.RemoveListener(Notices.UINotice.INFO_PAGE_PERSON_PROFILE_CLOSED, PageUpdate);
                Messenger.RemoveListener(Notices.UINotice.EDIT_PAGE_PERSON_PROFILE_CLOSED, PageUpdate);
                PageOpened.AddListener(PageUpdate);
            }
            catch { }
        }

        #endregion


        public override void PageUpdate()
        {
            string search = inputFieldSearch.text;

            Debug.Log("Обновление списков");

            persons = Services.EntityData.LoadPersonList(search);

            ContentRecreate();
        }

        private void ContentRecreate()
        {
            Debug.Log($"[{typeof(Page_PersonList).Name}] Operation: update list elements\nElements count: {persons.Count}");

            // Настройка заполнения
            verticalLayout.Clear();
            verticalLayout.Generate(_listelementPerson, persons.Count, (index, iCell) =>
            {
                var regularCell = iCell as UI_ListElement_PersonNote;
                if (regularCell != null)
                {
                    Vector3 vector3 = regularCell.transform.position;
                    vector3.z = 0f;
                    regularCell.transform.position = vector3;
                    regularCell.onGenerated?.Invoke(index);
                    regularCell.UpdateData(persons[index]);
                }
            });

            UnlimitedScrollerSetSize();
        }

        /// <summary>
        /// Настройка размеров панели элементов списка
        /// </summary>
        private void UnlimitedScrollerSetSize()
        {
            // Настройка размеров
            var rectSource = panelSearch.GetComponent<RectTransform>();
            var rectTarget = verticalLayout.GetComponent<RectTransform>();
            Vector2 sizeDelta = rectTarget.sizeDelta;
            sizeDelta.x = rectSource.sizeDelta.x;
            rectTarget.sizeDelta = sizeDelta;
        }

        private void PageUpdate(Page page)
        {
            PageUpdate();
        }
    }
}
