using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_ContactInfo : UI_Card<ContactInfo>
    {
        [Header("Settings")]
        [SerializeField] private ContactTypeEnum contactType;

        [Header("References")]
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text textContact;
        [SerializeField] private TMP_Text textContactType;

        [Header("Prefab References")]
        [SerializeField] private Sprite phoneIcon;
        [SerializeField] private Sprite socialMediaIcon;
        [SerializeField] private Sprite emailIcon;
        [SerializeField] private Sprite otherIcon;


        public ContactTypeEnum ContactType
        {
            get => contactType;
            set => contactType = value;
        }


        private void OnValidate()
        {
            SetImage(contactType);
        }


        public override void UpdateData(ContactInfo contactInfo)
        {
            base.UpdateData(contactInfo);

            contactType = contactInfo.ContactType;
            textContactType.text = contactInfo.ContactSubtype;
            textContact.text = contactInfo.ContactData;

            SetImage(contactType);
        }

        private void SetImage(ContactTypeEnum contactType)
        {
            image.sprite = contactType switch
            {
                ContactTypeEnum.SocialNetwork => socialMediaIcon,
                ContactTypeEnum.Phone => phoneIcon,
                ContactTypeEnum.Email => emailIcon,
                ContactTypeEnum.Other => otherIcon,
                _ => null
            };
        }

        public void CopyToBuffer()
        {
            UniClipboard.SetText(textContact.text);
        }
    }
}
