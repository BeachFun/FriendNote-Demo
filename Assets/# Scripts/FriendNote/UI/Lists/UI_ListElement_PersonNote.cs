using FriendNote.Core.DTO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnlimitedScrollUI;

namespace FriendNote.UI
{
    public class UI_ListElement_PersonNote : RegularCell
    {
        [SerializeField] private Image imageIcon;
        [SerializeField] private TMP_Text textNickname;
        [SerializeField] private TMP_Text textRate;

        private PersonBasicInfo _personInfo;

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

        public void UpdateData(PersonBasicInfo person)
        {
            _personInfo = person;

            textNickname.text = string.IsNullOrEmpty(person.Nickname) ? $"{person.Surname} {person.Name}" : person.Nickname;
        }

        public void OnClick()
        {
            if (_personInfo.Id.HasValue)
            {
                Controllers.Pages.OpenInfoPage<PersonProfile>(_personInfo.Id.Value);
            }
        }
    }
}
