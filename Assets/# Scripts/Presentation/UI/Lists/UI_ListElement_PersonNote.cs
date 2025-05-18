using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnlimitedScrollUI;
using Zenject;

namespace FriendNote.UI
{
    public class UI_ListElement_PersonNote : RegularCell
    {
        [SerializeField] private Image imageIcon;
        [SerializeField] private TMP_Text textNickname;
        [SerializeField] private TMP_Text textRate;

        [Inject] private IPagesController pagesController;

        private Person _personInfo;

        public string Text
        {
            get => textNickname.text;
            set => textNickname.text = value;
        }
        public string TextRate
        {
            get => textRate.text;
            set => textRate.text = value;
        }

        public void UpdateData(Person person)
        {
            _personInfo = person;

            textNickname.text = string.IsNullOrEmpty(person.Nickname) ? $"{person.Surname} {person.Name}" : person.Nickname;
        }

        public void OnClick()
        {
            if (_personInfo.Id.HasValue)
            {
                pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<PersonProfile>(), _personInfo.Id.Value);
            }
        }
    }
}
