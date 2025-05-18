using System;
using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Data.Repositories;
using FriendNote.Domain;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonProfile : EditPage<PersonProfile>
    {
        // TODO: ����� ����������� ������� ��������� ����� ����� � ����� ��� ������������� �� onEndEdit � ��������� �� IsContainValidData

        [Header("Required Page Fields References")]
        [SerializeField] private RUI.InputField fieldNickname;
        [SerializeField] private RUI.InputField fieldName;
        [SerializeField] private RUI.InputField fieldSurname;
        [SerializeField] private RUI.InputField fieldPatronymic;
        [SerializeField] private Toggle toggleMale;
        [SerializeField] private Toggle toggleFemale;
        [SerializeField] private TMP_Dropdown dropdownRelationshipType;
        [SerializeField] private RUI.InputField fieldAcquaintance;
        [SerializeField] private UI_DatePicker datePickerBirthDate;
        [SerializeField] private UI_DropdownContactGroup inputGroupPhone;
        [SerializeField] private UI_DropdownContactGroup inputGroupSocialNetwork;
        [SerializeField] private UI_DropdownContactGroup inputGroupEmail;
        [SerializeField] private RUI.InputField fieldNotation;

        [Inject] private IRepositoryFactory _repoFactory;
        [Inject] private IDataService dataService;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            InitializeFields();

            fieldNickname.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldSurname.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldPatronymic.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldNotation.onValueChanged.AddListener(x => ShowSaveButtons());

            toggleMale.onValueChanged.AddListener(x => ShowSaveButtons());
            toggleFemale.onValueChanged.AddListener(x => ShowSaveButtons());

            inputGroupPhone.OnEndEdit.AddListener(x => ShowSaveButtons());
            inputGroupSocialNetwork.OnEndEdit.AddListener(x => ShowSaveButtons());
            inputGroupEmail.OnEndEdit.AddListener(x => ShowSaveButtons());

            datePickerBirthDate.onEndEdit.AddListener(x => ShowSaveButtons());
        }

        private void InitializeFields()
        {
            dropdownRelationshipType.ClearOptions();
            dropdownRelationshipType.AddOptions(EnumConverterStrategy<RelationshipTypeEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null || _pageData.Value.BasicInfo is null) return;

            Person basicInfo = _pageData.Value.BasicInfo;

            fieldNickname.Text = basicInfo.Nickname;
            fieldName.Text = basicInfo.Name;
            fieldSurname.Text = basicInfo.Surname;
            fieldPatronymic.Text = basicInfo.Patronymic;


            if (basicInfo.Gender.HasValue) switch (basicInfo.Gender.Value)
                {
                    case GenderEnum.Male:
                        toggleMale.isOn = true;
                        break;
                    case GenderEnum.Female:
                        toggleFemale.isOn = true;
                        break;

                    default:
                        toggleMale.isOn = false;
                        toggleFemale.isOn = false;
                        break;
                }

            if (basicInfo.Birthdate.HasValue)
            {
                datePickerBirthDate.Date = basicInfo.Birthdate.Value;
            }

            if (basicInfo.RelationshipType.HasValue)
            {
                string str = EnumConverterStrategy<RelationshipTypeEnum>.ToString(basicInfo.RelationshipType.Value);

                dropdownRelationshipType.value = dropdownRelationshipType.options
                    .Select(x => x.text)
                    .ToList()
                    .IndexOf(str);
            }

            fieldAcquaintance.Text = basicInfo.Acquaintance;

            // ���������� ��������� ����������
            PersonContacts contacts = _pageData.Value.ContactInfo;
            if (contacts is not null)
            {
                inputGroupPhone.Contacts = contacts.PhoneNumbers;
                inputGroupSocialNetwork.Contacts = contacts.SocialMedia;
                inputGroupEmail.Contacts = contacts.Emails;
            }

            fieldNotation.Text = basicInfo.Notation;
        }

        public override void ResetFields()
        {
            fieldNickname.Text = string.Empty;
            fieldName.Text = string.Empty;
            fieldSurname.Text = string.Empty;
            fieldPatronymic.Text = string.Empty;

            toggleMale.isOn = false;
            toggleFemale.isOn = false;

            datePickerBirthDate.Date = null;

            dropdownRelationshipType.value = -1;
            fieldAcquaintance.Text = string.Empty;

            inputGroupPhone.Clear();
            inputGroupSocialNetwork.Clear();
            inputGroupEmail.Clear();

            fieldNotation.Text = string.Empty;
        }

        protected override bool CheckFillValidation()
        {
            return fieldNickname.TextIsValid &&
                fieldName.TextIsValid &&
                fieldSurname.TextIsValid &&
                fieldPatronymic.TextIsValid &&
                fieldNotation.TextIsValid &&
                fieldAcquaintance.TextIsValid &&
                inputGroupPhone.IsContainValidData &&
                inputGroupSocialNetwork.IsContainValidData &&
                inputGroupEmail.IsContainValidData &&
                datePickerBirthDate.IsContainValidDate;
        }


        protected override PersonProfile CollectData()
        {
            // ��������� ���������� � ����
            GenderEnum gender = GenderEnum.Unknown;
            if (toggleMale.isOn)
                gender = GenderEnum.Male;
            if (toggleFemale.isOn)
                gender = GenderEnum.Female;

            // ����������� ���������� ���������� � �������������� ���� ����������
            var basicInfo = new Person()
            {
                Nickname = fieldNickname.Text,
                Name = fieldName.Text,
                Surname = fieldSurname.Text,
                Patronymic = fieldPatronymic.Text,
                Gender = gender,

                RelationshipType = dropdownRelationshipType.value == -1
                ? null
                : EnumConverterStrategy<RelationshipTypeEnum>.ToEnum(dropdownRelationshipType.options[dropdownRelationshipType.value].text),

                Acquaintance = fieldAcquaintance.Text,
                Birthdate = datePickerBirthDate.Date,
                Notation = fieldNotation.Text
            };

            // �������� Id ��������, ���� ���� ������� � ������ ��������������
            if (this.EditMode == EditModeEnum.Updating)
            {
                basicInfo.Id = _pageData.Value.BasicInfo.Id;

                // �������� �� �� ���������� �� Relatinoship ��� Updating mode
                if (_pageData.Value.BasicInfo.RelationshipType != basicInfo.RelationshipType)
                {
                    basicInfo.dateEditRelationshipType = DateTime.Now;
                }
            }

            var person = new PersonProfile()
            {
                BasicInfo = basicInfo,
                ContactInfo = CollectContacts()
            };

            return person;
        }

        /// <summary>
        /// �������� �������� ���������� ������
        /// </summary>
        private PersonContacts CollectContacts()
        {
            var result = EditMode == EditModeEnum.Updating
                ? _pageData.Value.ContactInfo // ���������
                : new PersonContacts(); // ����������

            result.PhoneNumbers = inputGroupPhone.Contacts;
            result.SocialMedia = inputGroupSocialNetwork.Contacts;
            result.Emails = inputGroupEmail.Contacts;

            return result;
        }


        public override void SaveData()
        {
            if (!CheckFillValidation()) return;

            // �������� ���� ���������� ������, ����� ����������� ����������
            if (EditMode == EditModeEnum.Updating)
            {
                // Id ������ ����, ��� ��� ���� ������� � ������ ��������������
                var entityRepo = _repoFactory.GetRelatedRepo<ContactInfo>();
                var contacts = entityRepo.LoadAllByPersonId(_pageData.Value.BasicInfo.Id.Value);
                if (contacts is not null) foreach (var contact in contacts)
                    {
                        entityRepo.Remove(contact);
                    }
            }

            if (dataService.SavePersonProfile(CollectData()))
            {
                ResetFields();
            }
        }
    }
}
