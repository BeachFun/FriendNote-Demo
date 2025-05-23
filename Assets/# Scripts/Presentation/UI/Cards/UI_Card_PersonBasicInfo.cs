using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_Card_PersonBasicInfo : UI_Card<Person>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textFieldSurname;
        [SerializeField] private TMP_Text textFieldName;
        [SerializeField] private TMP_Text textFieldPatronymic;
        [SerializeField] private TMP_Text textFieldGender;
        [SerializeField] private TMP_Text textFieldRelationshipType;
        [SerializeField] private TMP_Text textFieldAcquaintance;
        [SerializeField] private TMP_Text textFieldBirthDate;

        private readonly string _emptyField = "???";


        public override void UpdateData(Person personInfo)
        {
            base.UpdateData(personInfo);

            textFieldSurname.text = string.IsNullOrEmpty(personInfo.Surname)
                ? _emptyField
                : personInfo.Surname;

            textFieldName.text = string.IsNullOrEmpty(personInfo.Name)
                ? _emptyField
                : personInfo.Name;

            textFieldPatronymic.text = string.IsNullOrEmpty(personInfo.Patronymic)
                ? _emptyField
                : personInfo.Patronymic;

            if (personInfo.Gender.HasValue)
            {
                textFieldGender.text = personInfo.Gender.Value switch
                {
                    GenderEnum.Male => "Муж.",
                    GenderEnum.Female => "Жен.",
                    _ => _emptyField
                };
            }
            else
            {
                textFieldGender.text = _emptyField;
            }

            textFieldRelationshipType.text = personInfo.RelationshipType.HasValue
                ? EnumConverterStrategy<RelationshipTypeEnum>.ToString(personInfo.RelationshipType.Value)
                : _emptyField;

            textFieldAcquaintance.text = string.IsNullOrEmpty(personInfo.Acquaintance)
                ? _emptyField
                : personInfo.Acquaintance;

            textFieldBirthDate.text = personInfo.Birthdate.HasValue
                ? personInfo.Birthdate?.ToString("dd.MM.yyyy")
                : _emptyField;
        }
    }
}
