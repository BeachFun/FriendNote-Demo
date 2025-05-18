using FriendNote.Domain.DTO;
using FriendNote.UI.Cards;
using UnityEngine;

namespace FriendNote.UI
{
    public class UI_Section_ContactInfo : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject phoneNumbersParent;
        [SerializeField] private GameObject socialMediaParent;
        [SerializeField] private GameObject emailsParent;
        [SerializeField] private GameObject otherContactsParent;

        [Header("Prefab References")]
        [SerializeField] private GameObject cardContactInfo;


        private void Awake()
        {
            ClearContactsCards();
        }

        public void UpdateData(PersonContacts contactInfo)
        {
            ClearContactsCards();
            GenerateContactsCards(contactInfo);
        }

        private void ClearContactsCards()
        {
            foreach (var contactObj in phoneNumbersParent.GetComponentsInChildren<UI_PersonCard_ContactInfo>())
            {
                DestroyImmediate(contactObj.gameObject);
            }
            foreach (var contactObj in socialMediaParent.GetComponentsInChildren<UI_PersonCard_ContactInfo>())
            {
                DestroyImmediate(contactObj.gameObject);
            }
            foreach (var contactObj in emailsParent.GetComponentsInChildren<UI_PersonCard_ContactInfo>())
            {
                DestroyImmediate(contactObj.gameObject);
            }
            foreach (var contactObj in otherContactsParent.GetComponentsInChildren<UI_PersonCard_ContactInfo>())
            {
                DestroyImmediate(contactObj.gameObject);
            }
        }

        private void GenerateContactsCards(PersonContacts personContacts)
        {
            if (personContacts == null) return;

            UI_PersonCard_ContactInfo cardContact;

            // ��������� �������� � ����������� ��������
            if (personContacts.PhoneNumbers is not null && personContacts.PhoneNumbers.Count > 0)
            {
                phoneNumbersParent.SetActive(true);
                foreach (var contact in personContacts.PhoneNumbers)
                {
                    cardContact = Instantiate(cardContactInfo, phoneNumbersParent.transform).GetComponent<UI_PersonCard_ContactInfo>();
                    cardContact.UpdateData(contact);
                }
            }
            else
            {
                phoneNumbersParent.SetActive(false);
            }

            // ��������� �������� � ����������� ������
            if (personContacts.SocialMedia is not null && personContacts.SocialMedia.Count > 0)
            {
                socialMediaParent.SetActive(true);
                foreach (var contact in personContacts.SocialMedia)
                {
                    cardContact = Instantiate(cardContactInfo, socialMediaParent.transform).GetComponent<UI_PersonCard_ContactInfo>();
                    cardContact.UpdateData(contact);
                }
            }
            else
            {
                socialMediaParent.SetActive(false);
            }

            // ��������� �������� � ������������ �������
            if (personContacts.Emails is not null && personContacts.Emails.Count > 0)
            {
                emailsParent.SetActive(true);
                foreach (var contact in personContacts.Emails)
                {
                    cardContact = Instantiate(cardContactInfo, emailsParent.transform).GetComponent<UI_PersonCard_ContactInfo>();
                    cardContact.UpdateData(contact);
                }
            }
            else
            {
                emailsParent.SetActive(false);
            }

            // ��������� �������� � ���������� ����������� �������
            if (personContacts.Others is not null && personContacts.Others.Count > 0)
            {
                otherContactsParent.SetActive(true);
                foreach (var contact in personContacts.Others)
                {
                    cardContact = Instantiate(cardContactInfo, otherContactsParent.transform).GetComponent<UI_PersonCard_ContactInfo>();
                    cardContact.UpdateData(contact);
                }
            }
            else
            {
                otherContactsParent.SetActive(false);
            }
        }
    }
}
