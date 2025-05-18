using System.Collections.Generic;
using FriendNote.Data.Repositories;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using FriendNote.UI.Cards;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FriendNote.UI.Pages
{
    /// <summary>
    /// ����� ������ ���������� � �������� | Residences, Education, Career, Skills, Interests, Goals,
    /// </summary>
    public abstract class EntityRelatedInfoPage<T> : PersonInfoPage where T : EntityBase, IPersonRelatedInfo, new()
    {
        [Header("References")]
        [SerializeField] protected TMP_Text textNickname;
        [SerializeField] protected VerticalLayoutGroup cardContainer;
        [SerializeField] protected Button buttonAddPerson;

        [Header("Prefab references")]
        [SerializeField] protected GameObject cardToGenerating;

        protected Person _personInfo = new();

        private IPersonRelatedInfoRepository<T> repo;
        [Inject] DiContainer container;
        [Inject] IRepositoryFactory repoFactory;
        [Inject] private IPagesController pagesController;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            ClearCards();
        }

        protected override void Start()
        {
            repo = repoFactory.GetRelatedRepo<T>();

            base.Start(); // Вызов после получения репозитория, так как во время вызова base.Start происходит вызов Refresh, который использует репозиторий

            pagesController.OpenedPages
                .ObserveRemove()
                .Subscribe(x => Refresh())
                .AddTo(this);
        }

        #endregion


        public override void UpdateData(int personId)
        {
            base.UpdateData(personId);

            buttonAddPerson.onClick.AddListener(() =>
            {
                var page = pagesController.OpenEditPage<T>(PageRegistry.GetEditPageId<T>(), null) as EntityEditPage<T>;
                page.PersonId = _pageData.Value;
            });
        }

        public override void Refresh()
        {
            if (_pageData.Value < 0) return;

            _personInfo = repoFactory.GetPersonRepo().LoadById(_pageData.Value);

            if (_personInfo is null) return;

            textNickname.text = string.IsNullOrEmpty(_personInfo.Nickname) ?
                $"{_personInfo.Surname ?? _emptyField} {_personInfo.Name ?? _emptyField}"
                : _personInfo.Nickname;

            ClearCards();

            if (repo is not null)
                GenerateCards(repo.LoadAllByPersonId(_pageData.Value));
        }


        protected virtual void ClearCards()
        {
            foreach (var card in cardContainer.GetComponentsInChildren<UI_DataPanel>())
            {
#if UNITY_EDITOR
                DestroyImmediate(card.gameObject);
#else
                Destroy(card.gameObject);
#endif
            }
        }

        protected virtual void GenerateCards(IEnumerable<T> data)
        {
            if (cardToGenerating is null) return;

            UI_PersonCard<T> card;
            foreach (T cardData in data)
            {
                card = Instantiate(cardToGenerating, cardContainer.transform).GetComponent<UI_PersonCard<T>>();
                container.Inject(card);
                card.PersonId = this._pageData.Value;
                card.UpdateData(cardData);
            }

            //cardContainer.CalculateLayoutInputVertical();
        }
    }
}
