using System.Collections.Generic;
using System.Linq;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using RUI;
using UnityEngine;

namespace FriendNote.UI
{
    public class UI_DropdownContactGroup : UI_DropdownGroup
    {
        [Space, Header("Settings")]
        [SerializeField] private ContactTypeEnum contactType;

        private List<ContactInfo> _contactList = new();

        public int PersonId { get; set; }
        public ContactTypeEnum ContactType
        {
            get => contactType;
            set => contactType = value;
        }

        public List<ContactInfo> Contacts
        {
            get
            {
                if (_contactList.Count > InputFieldsData.Length)
                {
                    _contactList = _contactList.Take(InputFieldsData.Length).ToList();
                }
                while (_contactList.Count < InputFieldsData.Length)
                {
                    _contactList.Add(new ContactInfo()
                    {
                        PersonId = this.PersonId,
                        ContactType = this.ContactType
                    });
                }

                for (int i = 0; i < InputFieldsData.Length; i++)
                {
                    _contactList[i].ContactSubtype = InputFieldsData[i].Item1;
                    _contactList[i].ContactData = InputFieldsData[i].Item2;
                }

                return _contactList;
            }
            set
            {
                _contactList = value;
                InputFieldsData = value is null ? null : value.Select(x => (x.ContactSubtype, x.ContactData)).ToArray(); // обновление значений полей
            }
        }


        public void OnEnable()
        {
            if (_contactList is null || _contactList.Count == 0) return;

            InputFieldsData = _contactList.Select(x => (x.ContactSubtype, x.ContactData)).ToArray(); // обновление значений полей
        }
    }
}
