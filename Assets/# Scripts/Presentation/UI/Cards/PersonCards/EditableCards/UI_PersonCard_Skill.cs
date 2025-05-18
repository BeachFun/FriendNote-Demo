using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_Skill : UI_PersonCard<Skill>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textSkillName;
        [SerializeField] private TMP_Text textSkillLevel;
        [SerializeField] private TMP_Text textDescription;
        [SerializeField] private TMP_Text textUsageScope;


        public override void UpdateData(Skill data)
        {
            base.UpdateData(data);

            textSkillName.text = string.IsNullOrEmpty(data.Name)
                ? _emptyField
                : data.Name;

            textSkillLevel.text = data.Level.HasValue
                ? EnumConverterStrategy<SkillLevelEnum>.ToString(data.Level.Value) ?? _emptyField
                : _emptyField;

            textDescription.text = string.IsNullOrEmpty(data.Description)
                ? _emptyField
                : data.Description;

            textUsageScope.text = string.IsNullOrEmpty(data.UsageScope)
                ? _emptyField
                : data.UsageScope;
        }
    }
}
