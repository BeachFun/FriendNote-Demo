using System.Linq;
using FriendNote.Core.DTO;
using FriendNote.Core.Enums;
using FriendNote.Core.Converters;
using Observer;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonResidence : RelatedInfoEditPage<Residence>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private UI_EditSection_Address sectionGeoplace;
        [SerializeField] private TMP_Dropdown dropdownPartOfCity;
        [SerializeField] private TMP_Dropdown dropdownApartmentType;
        [SerializeField] private UI_DatePicker datepickerStart;
        [SerializeField] private UI_DatePicker datepickerEnd;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            sectionGeoplace.OnValueChanged.AddListener(OnValueChanged);
            dropdownPartOfCity.onValueChanged.AddListener(OnValueChanged);
            dropdownApartmentType.onValueChanged.AddListener(OnValueChanged);
            datepickerStart.onValueChanged.AddListener(OnValueChanged);
            datepickerEnd.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            // Отписка от событий изменения данных в полях ввода
            sectionGeoplace.OnValueChanged.RemoveListener(OnValueChanged);
            dropdownPartOfCity.onValueChanged.RemoveListener(OnValueChanged);
            dropdownApartmentType.onValueChanged.RemoveListener(OnValueChanged);
            datepickerStart.onValueChanged.RemoveListener(OnValueChanged);
            datepickerEnd.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void InitializeFields()
        {
            dropdownPartOfCity.ClearOptions();
            dropdownPartOfCity.AddOptions(EnumConverterStrategy<CityPartEnum>.Data.Values.ToList());

            dropdownApartmentType.ClearOptions();
            dropdownApartmentType.AddOptions(EnumConverterStrategy<ApartmentsEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_RESIDENCE_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            // Обновление полей для ввода адреса проживания
            sectionGeoplace.DataUpdate(_pageData.Address);

            // Обновление поля для ввода части города
            string cityPartStr = _pageData.PartOfCity.HasValue
                ? EnumConverterStrategy<CityPartEnum>.ToString(_pageData.PartOfCity.Value)
                : null;
            dropdownPartOfCity.value = dropdownPartOfCity.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(cityPartStr);

            // Обновление поля для ввода вида проживания
            string apartmentStr = _pageData.ApartmentType.HasValue
                ? EnumConverterStrategy<ApartmentsEnum>.ToString(_pageData.ApartmentType.Value)
                : null;
            dropdownApartmentType.value = dropdownApartmentType.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(apartmentStr);

            // Обновление полей для ввода периода проживания
            datepickerStart.Date = _pageData.StartDate;
            datepickerEnd.Date = _pageData.EndDate;
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
                data.Id = _pageData.Id;
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
