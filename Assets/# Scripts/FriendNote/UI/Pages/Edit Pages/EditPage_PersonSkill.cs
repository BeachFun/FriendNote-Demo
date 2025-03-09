using System.Linq;
using FriendNote.Core.DTO;
using FriendNote.Core.Enums;
using FriendNote.Core.Converters;
using Observer;
using RUI;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonSkill : RelatedInfoEditPage<Skill>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCategory;
        [SerializeField] private InputField fieldName;
        [SerializeField] private InputField fieldDesc;
        [SerializeField] private TMP_Dropdown dropdownLevel;
        [SerializeField] private InputField fieldUsageScope;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            fieldCategory.onValueChanged.AddListener(OnValueChanged);
            fieldName.onValueChanged.AddListener(OnValueChanged);
            fieldDesc.onValueChanged.AddListener(OnValueChanged);
            dropdownLevel.onValueChanged.AddListener(OnValueChanged);
            fieldUsageScope.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            // Отписка от событий изменения данных в полях ввода
            fieldCategory.onValueChanged.RemoveListener(OnValueChanged);
            fieldName.onValueChanged.RemoveListener(OnValueChanged);
            fieldDesc.onValueChanged.RemoveListener(OnValueChanged);
            dropdownLevel.onValueChanged.RemoveListener(OnValueChanged);
            fieldUsageScope.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void InitializeFields()
        {
            dropdownLevel.ClearOptions();
            dropdownLevel.AddOptions(EnumConverterStrategy<SkillLevelEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_SKILL_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            fieldCategory.Text = _pageData.Category;
            fieldName.Text = _pageData.Name;
            fieldDesc.Text = _pageData.Description;
            fieldUsageScope.Text = _pageData.UsageScope;

            // Обновление поля для ввода уровня навыка/умения
            string skillLevelStr = _pageData.Level.HasValue
                ? EnumConverterStrategy<SkillLevelEnum>.ToString(_pageData.Level.Value)
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
                data.Id = _pageData.Id;
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
