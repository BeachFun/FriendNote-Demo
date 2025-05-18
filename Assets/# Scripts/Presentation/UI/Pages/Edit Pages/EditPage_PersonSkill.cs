using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using RUI;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonSkill : EntityEditPage<Skill>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCategory;
        [SerializeField] private InputField fieldName;
        [SerializeField] private InputField fieldDesc;
        [SerializeField] private TMP_Dropdown dropdownLevel;
        [SerializeField] private InputField fieldUsageScope;


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
            fieldUsageScope.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        private void InitializeFields()
        {
            dropdownLevel.ClearOptions();
            dropdownLevel.AddOptions(EnumConverterStrategy<SkillLevelEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            fieldCategory.Text = _pageData.Value.Category;
            fieldName.Text = _pageData.Value.Name;
            fieldDesc.Text = _pageData.Value.Description;
            fieldUsageScope.Text = _pageData.Value.UsageScope;

            // Обновление поля для ввода уровня навыка/умения
            string skillLevelStr = _pageData.Value.Level.HasValue
                ? EnumConverterStrategy<SkillLevelEnum>.ToString(_pageData.Value.Level.Value)
                : null;
            dropdownLevel.value = dropdownLevel.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(skillLevelStr);
        }

        public override void ResetFields()
        {
            fieldCategory.Text = string.Empty;
            fieldName.Text = string.Empty;
            fieldDesc.Text = string.Empty;
            fieldUsageScope.Text = string.Empty;
            dropdownLevel.value = -1;
        }

        protected override Skill CollectData()
        {
            var data = new Skill()
            {
                PersonId = this.PersonId,
                Category = fieldCategory.Text,
                Name = fieldName.Text,
                Description = fieldDesc.Text,

                Level = dropdownLevel.value == -1
                ? null
                : EnumConverterStrategy<SkillLevelEnum>.ToEnum(dropdownLevel.options[dropdownLevel.value].text),

                UsageScope = fieldUsageScope.Text
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
                fieldDesc.TextIsValid &&
                fieldUsageScope.TextIsValid;
        }
    }
}
