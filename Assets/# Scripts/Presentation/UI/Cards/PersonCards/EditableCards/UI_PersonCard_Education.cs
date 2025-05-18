using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_Education : UI_PersonCard<Education>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textInstitutName;
        [SerializeField] private TMP_Text textInstitutType;
        [SerializeField] private TMP_Text textInstitutAddress;
        [SerializeField] private TMP_Text textDirection;
        [SerializeField] private TMP_Text textSpecialization;
        [SerializeField] private TMP_Text textDegree;
        [SerializeField] private TMP_Text textStudyPeriod;


        public override void UpdateData(Education data)
        {
            base.UpdateData(data);

            textInstitutName.text = string.IsNullOrEmpty(data.InstitutionName)
                ? _emptyField
                : data.InstitutionName;

            textInstitutType.text = data.InstitutionType.HasValue
                ? EnumConverterStrategy<InstitutionTypeEnum>.ToString(data.InstitutionType.Value) ?? _emptyField
                : _emptyField;

            textInstitutAddress.text = string.IsNullOrEmpty(data.InstitutionAddress.GetAddressString())
                ? _emptyField
                : data.InstitutionAddress.GetAddressString();

            textDirection.text = string.IsNullOrEmpty(data.Direction)
                ? _emptyField
                : data.Direction;

            textSpecialization.text = string.IsNullOrEmpty(data.Specialization)
                ? _emptyField
                : data.Specialization;

            textDegree.text = data.Degree.HasValue
                ? EnumConverterStrategy<EducationDegreeEnum>.ToString(data.Degree.Value) ?? _emptyField
                : _emptyField;

            textStudyPeriod.text = string.IsNullOrEmpty(data.GetPeriod())
                ? _emptyField
                : data.GetPeriod();
        }
    }
}
