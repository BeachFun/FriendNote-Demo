using System.Collections.Generic;
using System.Linq;
using FriendNote.Data.Repositories;
using FriendNote.Domain.DTO;
using TMPro;
using UniRx;
using UnityEngine;
using UnlimitedScrollUI;
using Zenject;

namespace FriendNote.UI.Pages
{
    [ExecuteAlways]
    public class Page_PersonList : Page
    {
        [Header("Bindings")]
        [SerializeField] private GameObject panelSearch;
        [SerializeField] private TMP_InputField inputFieldSearch;
        [Tooltip("������ � ������� ����� ����������� ������")]
        [SerializeField] private VerticalUnlimitedScroller verticalLayout;

        [Header("References")]
        [SerializeField] private GameObject _listelementPerson;

        private List<Person> persons = new();

        [Inject] private DiContainer _container;
        [Inject] private IRepositoryFactory _repoFactory;
        [Inject] private IPagesController pagesController;


        #region [Методы] Запуск и инициализация сервиса

        private void Awake()
        {
            if (!Application.isPlaying) return;

            // �������� ���� �������� ��������� ������
            UnityHelper.DestroyAllChildren(verticalLayout.gameObject);

            pagesController.OpenedPages
                .ObserveRemove()
                .Subscribe(x => Refresh())
                .AddTo(this);
        }

        protected override void Start()
        {
            base.Start();

            // ��������� ��������
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

        #endregion


        private void OnEnable()
        {
            Refresh(); // Обновление при переходах между статическими страницами в Tab Menu
        }

        public override void Refresh()
        {
            if (!Application.isPlaying) return;

            string search = inputFieldSearch.text;

            Debug.Log("Page Info: страница PersonList обновлена");

            persons = _repoFactory.GetPersonRepo().LoadAllPersons(search).ToList();

            ContentRecreate();
        }

        private void ContentRecreate()
        {
            Debug.Log($"[{typeof(Page_PersonList).Name}] Operation: update list elements\nElements count: {persons.Count}");

            // ��������� ����������
            verticalLayout.Clear();
            verticalLayout.Generate(_listelementPerson, persons.Count, (index, iCell) =>
            {
                var regularCell = iCell as UI_ListElement_PersonNote;
                if (regularCell != null)
                {
                    _container.Inject(regularCell);

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
        /// ��������� �������� ������ ��������� ������
        /// </summary>
        private void UnlimitedScrollerSetSize()
        {
            // ��������� ��������
            var rectSource = panelSearch.GetComponent<RectTransform>();
            var rectTarget = verticalLayout.GetComponent<RectTransform>();
            Vector2 sizeDelta = rectTarget.sizeDelta;
            sizeDelta.x = rectSource.sizeDelta.x;
            rectTarget.sizeDelta = sizeDelta;
        }
    }
}
