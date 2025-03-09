using System;
using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.DTO;
using FriendNote.Core.Enums;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonProfile : EditPage<PersonProfile>
    {
        // TODO: можно попробовать создать коллекцию полей ввода и через нее подписываться на onEndEdit и проверять на IsContainValidData

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


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            InitializeFields();

            fieldNickname.onValueChanged.AddListener(OnValueChanged);
            fieldName.onValueChanged.AddListener(OnValueChanged);
            fieldSurname.onValueChanged.AddListener(OnValueChanged);
            fieldPatronymic.onValueChanged.AddListener(OnValueChanged);
            fieldNotation.onValueChanged.AddListener(OnValueChanged);

            toggleMale.onValueChanged.AddListener(OnValueChanged);
            toggleFemale.onValueChanged.AddListener(OnValueChanged);

            inputGroupPhone.OnEndEdit.AddListener(OnValueChanged);
            inputGroupSocialNetwork.OnEndEdit.AddListener(OnValueChanged);
            inputGroupEmail.OnEndEdit.AddListener(OnValueChanged);

            datePickerBirthDate.onEndEdit.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            fieldNickname.onValueChanged.RemoveListener(OnValueChanged);
            fieldName.onValueChanged.RemoveListener(OnValueChanged);
            fieldSurname.onValueChanged.RemoveListener(OnValueChanged);
            fieldPatronymic.onValueChanged.RemoveListener(OnValueChanged);
            fieldNotation.onValueChanged.RemoveListener(OnValueChanged);

            toggleMale.onValueChanged.RemoveListener(OnValueChanged);
            toggleFemale.onValueChanged.RemoveListener(OnValueChanged);

            inputGroupPhone.OnEndEdit.RemoveListener(OnValueChanged);
            inputGroupSocialNetwork.OnEndEdit.RemoveListener(OnValueChanged);
            inputGroupEmail.OnEndEdit.RemoveListener(OnValueChanged);

            datePickerBirthDate.onEndEdit.RemoveListener(OnValueChanged);
        }

        private void InitializeFields()
        {
            dropdownRelationshipType.ClearOptions();
            dropdownRelationshipType.AddOptions(EnumConverterStrategy<RelationshipTypeEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_PROFILE_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            if (_pageData is null || _pageData.BasicInfo is null) return;

            PersonBasicInfo basicInfo = _pageData.BasicInfo;

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

            // Обновление контакной информации
            PersonContacts contacts = _pageData.ContactInfo;
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

        /// <summary>
        /// Проверка корректного заполнения полей для ввода данных страницы
        /// </summary>
        /// <returns>true - good, false - bad</returns>
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
            // Получение информации о поле
            GenderEnum gender = GenderEnum.Unknown;
            if (toggleMale.isOn)
                gender = GenderEnum.Male;
            if (toggleFemale.isOn)
                gender = GenderEnum.Female;

            // Группировка полученной информации и дополнительный сбор информации
            var basicInfo = new PersonBasicInfo()
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

            // Указание Id человека, если окно открыто в режиме редактирования
            if (this.EditMode == EditModeEnum.Updating)
            {
                basicInfo.Id = _pageData.BasicInfo.Id;

                // Проверка на то поменялось ли Relatinoship при Updating mode
                if (_pageData.BasicInfo.RelationshipType != basicInfo.RelationshipType)
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
        /// Отдельно собирает контактные данные
        /// </summary>
        private PersonContacts CollectContacts()
        {
            var result = EditMode == EditModeEnum.Updating
                ? _pageData.ContactInfo // изменение
                : new PersonContacts(); // добавление

            result.PhoneNumbers = inputGroupPhone.Contacts;
            result.SocialMedia = inputGroupSocialNetwork.Contacts;
            result.Emails = inputGroupEmail.Contacts;

            return result;
        }


        public override void SaveData()
        {
            if (!CheckFillValidation()) return;

            // Удаление всех контактных данных, перед сохранением оставшихся
            if (EditMode == EditModeEnum.Updating)
            {
                // Id должен быть, так как окно открыто в режиме редактирования
                var contacts = Services.EntityData.LoadPersonRelatedInfo<ContactInfo>(_pageData.BasicInfo.Id.Value);
                if (contacts is not null) foreach (var contact in contacts)
                    {
                        Services.EntityData.RemoveEntity(contact);
                    }
            }

            Services.Data.SavePersonProfile(CollectData());
        }
    }
}
