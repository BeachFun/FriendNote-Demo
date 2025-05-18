using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonResidence : EntityEditPage<Residence>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private UI_EditSection_Address sectionGeoplace;
        [SerializeField] private TMP_Dropdown dropdownPartOfCity;
        [SerializeField] private TMP_Dropdown dropdownApartmentType;
        [SerializeField] private UI_DatePicker datepickerStart;
        [SerializeField] private UI_DatePicker datepickerEnd;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            sectionGeoplace.OnValueChanged.AddListener(x => ShowSaveButtons());
            dropdownPartOfCity.onValueChanged.AddListener(x => ShowSaveButtons());
            dropdownApartmentType.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerStart.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerEnd.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        private void InitializeFields()
        {
            dropdownPartOfCity.ClearOptions();
            dropdownPartOfCity.AddOptions(EnumConverterStrategy<CityPartEnum>.Data.Values.ToList());

            dropdownApartmentType.ClearOptions();
            dropdownApartmentType.AddOptions(EnumConverterStrategy<ApartmentsEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            sectionGeoplace.UpdateData(_pageData.Value.Address);

            string cityPartStr = _pageData.Value.PartOfCity.HasValue
                ? EnumConverterStrategy<CityPartEnum>.ToString(_pageData.Value.PartOfCity.Value)
                : null;
            dropdownPartOfCity.value = dropdownPartOfCity.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(cityPartStr);

            string apartmentStr = _pageData.Value.ApartmentType.HasValue
                ? EnumConverterStrategy<ApartmentsEnum>.ToString(_pageData.Value.ApartmentType.Value)
                : null;
            dropdownApartmentType.value = dropdownApartmentType.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(apartmentStr);

            datepickerStart.Date = _pageData.Value.StartDate;
            datepickerEnd.Date = _pageData.Value.EndDate;
        }

        public override void ResetFields()
        {
            sectionGeoplace.ResetFields();
            dropdownPartOfCity.value = -1;
            dropdownApartmentType.value = -1;
            datepickerStart.Date = null;
            datepickerEnd.Date = null;
        }

        protected override Residence CollectData()
        {
            var data = new Residence()
            {
                PersonId = this.PersonId,
                Address = sectionGeoplace.CollectData(),

                PartOfCity = dropdownPartOfCity.value == -1
                ? null
                : EnumConverterStrategy<CityPartEnum>.ToEnum(dropdownPartOfCity.options[dropdownPartOfCity.value].text),

                ApartmentType = dropdownApartmentType.value == -1
                ? null
                : EnumConverterStrategy<ApartmentsEnum>.ToEnum(dropdownApartmentType.options[dropdownApartmentType.value].text),

                StartDate = datepickerStart.Date,
                EndDate = datepickerEnd.Date,
            };

            if (EditMode == EditModeEnum.Updating)
            {
                data.Id = _pageData.Value.Id;
            }

            return data;
        }

        protected override bool CheckFillValidation()
        {
            return sectionGeoplace.IsContainValidData &&
                datepickerStart.IsContainValidDate &&
                datepickerEnd.IsContainValidDate;
        }
    }
}
