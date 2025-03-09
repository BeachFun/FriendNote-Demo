using FriendNote.Core.DTO;
using FriendNote.Core.Enums;
using FriendNote.Core.Converters;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_Interest : UI_PersonCard<Interest>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textLevel;
        [SerializeField] private TMP_Text textDescription;


        public override void UpdateData(Interest data)
        {
            base.UpdateData(data);

            textName.text = string.IsNullOrEmpty(data.Name)
                ? _emptyField
                : data.Name;

            textLevel.text = data.Level.HasValue
                ? EnumConverterStrategy<InterestLevelEnum>.ToString(data.Level.Value) ?? _emptyField
                : _emptyField;

            textDescription.text = string.IsNullOrEmpty(data.Description)
                ? _emptyField
                : data.Description;
        }
    }
}
