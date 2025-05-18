using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using RUI;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonInterest : EntityEditPage<Interest>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCategory;
        [SerializeField] private InputField fieldName;
        [SerializeField] private InputField fieldDesc;
        [SerializeField] private TMP_Dropdown dropdownLevel;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            fieldCategory.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldDesc.onValueChanged.AddListener(x => ShowSaveButtons());
            dropdownLevel.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        private void InitializeFields()
        {
            dropdownLevel.ClearOptions();
            dropdownLevel.AddOptions(EnumConverterStrategy<InterestLevelEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            fieldCategory.Text = _pageData.Value.Category;
            fieldName.Text = _pageData.Value.Name;
            fieldDesc.Text = _pageData.Value.Description;

            // Обновление поля для ввода уровня интереса
            if (_pageData.Value.Level.HasValue)
            {
                string intersetLevelStr = EnumConverterStrategy<InterestLevelEnum>.ToString(_pageData.Value.Level.Value);

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
                data.Id = _pageData.Value.Id;
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
