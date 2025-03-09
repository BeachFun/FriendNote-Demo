using System.Linq;
using FriendNote.Core.DTO;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using Observer;
using RUI;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonInterest : RelatedInfoEditPage<Interest>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCategory;
        [SerializeField] private InputField fieldName;
        [SerializeField] private InputField fieldDesc;
        [SerializeField] private TMP_Dropdown dropdownLevel;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            fieldCategory.onValueChanged.AddListener(OnValueChanged);
            fieldName.onValueChanged.AddListener(OnValueChanged);
            fieldDesc.onValueChanged.AddListener(OnValueChanged);
            dropdownLevel.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            // Отписка от событий изменения данных в полях ввода
            fieldCategory.onValueChanged.RemoveListener(OnValueChanged);
            fieldName.onValueChanged.RemoveListener(OnValueChanged);
            fieldDesc.onValueChanged.RemoveListener(OnValueChanged);
            dropdownLevel.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void InitializeFields()
        {
            dropdownLevel.ClearOptions();
            dropdownLevel.AddOptions(EnumConverterStrategy<InterestLevelEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_INTEREST_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            fieldCategory.Text = _pageData.Category;
            fieldName.Text = _pageData.Name;
            fieldDesc.Text = _pageData.Description;

            // Обновление поля для ввода уровня интереса
            if (_pageData.Level.HasValue)
            {
                string intersetLevelStr = EnumConverterStrategy<InterestLevelEnum>.ToString(_pageData.Level.Value);

                dropdownLevel.value = dropdownLevel.options
                    .Select(x => x.text)
                    .ToList()
                    .IndexOf(intersetLevelStr);
            }
        }

        public override void ResetFields()
        {
            fieldCategory.Text = string.Empty;
            fieldName.Text = string.Empty;
            fieldDesc.Text = string.Empty;
            dropdownLevel.value = -1;
        }

        protected override Interest CollectData()
        {
            var data = new Interest()
            {
                PersonId = this.PersonId,
                Category = fieldCategory.Text,
                Name = fieldName.Text,
                Description = fieldDesc.Text,

                Level = dropdownLevel.value == -1
                ? null
                : EnumConverterStrategy<InterestLevelEnum>.ToEnum(dropdownLevel.options[dropdownLevel.value].text)
            };

            if (EditMode == EditModeEnum.Updating)
            {
                data.Id = _pageData.Id;
            }

            return data;
        }

        protected override bool CheckFillValidation()
        {
            return fieldCategory.TextIsValid &&
                fieldName.TextIsValid &&
                fieldDesc.TextIsValid;
        }
    }
}
