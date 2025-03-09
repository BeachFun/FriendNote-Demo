using System.Collections.Generic;
using FriendNote.Core.DTO;
using FriendNote.UI.Cards;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Pages
{
    /// <summary>
    /// Любой раздел информации о человеке | Residences, Education, Career, Skills, Interests, Goals,
    /// </summary>
    public abstract class EntityRelatedInfoPage<T> : InfoPage where T : EntityBase, IPersonRelatedInfo, new()
    {
        [Header("References")]
        [SerializeField] protected TMP_Text textNickname;
        [SerializeField] protected VerticalLayoutGroup cardContainer;
        [SerializeField] protected Button buttonAddPerson;

        [Header("Prefab references")]
        [SerializeField] protected GameObject cardToGenerating;

        protected PersonBasicInfo _personInfo;


        #region [Методы] Управление жизненным циклом

        protected virtual void Awake()
        {
            Messenger.AddListener(Notices.DataNotice.PERSONS_DATA_UPDATED, PageUpdate);
        }

        protected virtual void OnDestroy()
        {
            Messenger.RemoveListener(Notices.DataNotice.PERSONS_DATA_UPDATED, PageUpdate);
        }

        #endregion

        public override void DataUpdate(int personId)
        {
            _personInfo = Services.EntityData.LoadEntity<PersonBasicInfo>(personId);

            PageUpdate();

            buttonAddPerson.onClick.AddListener(() =>
            {
                Controllers.Pages.OpenRelatedInfoEditPage<T>(personId);
            });

            base.DataUpdate(personId);
        }

        public override void PageUpdate()
        {
            textNickname.text = string.IsNullOrEmpty(_personInfo.Nickname) ?
                $"{_personInfo.Surname ?? _emptyField} {_personInfo.Name ?? _emptyField}"
                : _personInfo.Nickname;

            ClearCards();
            GenerateCards(Services.EntityData.LoadPersonRelatedInfo<T>(_personInfo.Id.Value) ?? new());
        }


        protected virtual void ClearCards()
        {
            foreach (var card in cardContainer.GetComponentsInChildren<UI_DataPanel>())
            {
                DestroyImmediate(card.gameObject);
            }
        }

        protected virtual void GenerateCards(List<T> data)
        {
            if (cardToGenerating is null) return;

            UI_Card<T> card;
            foreach (T cardData in data)
            {
                card = Instantiate(cardToGenerating, cardContainer.transform).GetComponent<UI_Card<T>>();
                card.UpdateData(cardData);
            }

            cardContainer.CalculateLayoutInputVertical();
        }


        protected virtual void OnInfoUpdate()
        {
            DataUpdate(_personInfo.Id.Value);
        }
    }
}
