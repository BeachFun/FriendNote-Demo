using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_Residence : UI_PersonCard<Residence>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textApartmentsType;
        [SerializeField] private TMP_Text textPartOfCity;
        [SerializeField] private TMP_Text textAddress;
        [SerializeField] private TMP_Text textLifePeriod;


        public override void UpdateData(Residence residence)
        {
            base.UpdateData(residence);

            textApartmentsType.text = residence.ApartmentType.HasValue
                ? EnumConverterStrategy<ApartmentsEnum>.ToString(residence.ApartmentType.Value) ?? _emptyField
                : _emptyField;

            textPartOfCity.text = residence.PartOfCity.HasValue
                ? EnumConverterStrategy<CityPartEnum>.ToString(residence.PartOfCity.Value) ?? _emptyField
                : _emptyField;

            textAddress.text = string.IsNullOrEmpty(residence.Address.GetAddressString())
                ? _emptyField
                : residence.Address.GetAddressString();

            textLifePeriod.text = string.IsNullOrEmpty(residence.GetPeriod())
                ? _emptyField
                : residence.GetPeriod();
        }
    }
}
