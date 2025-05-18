using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.Enums;
using FriendNote.Domain.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonEducation : EntityEditPage<Education>
    {
        [Header("Required Fields Page references")]
        [SerializeField] private TMP_Dropdown dropdownInstitutType;
        [SerializeField] private RUI.InputField feldInstitutName;
        [SerializeField] private UI_EditSection_Address sectionGeoplace;
        [SerializeField] private RUI.InputField fieldDirection;
        [SerializeField] private RUI.InputField fieldSpecialization;
        [SerializeField] private TMP_Dropdown dropdownDegree;
        [SerializeField] private UI_DatePicker datepickerStartDate;
        [SerializeField] private UI_DatePicker datepickerEndDate;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            dropdownInstitutType.onValueChanged.AddListener(x => ShowSaveButtons());
            dropdownDegree.onValueChanged.AddListener(x => ShowSaveButtons());
            sectionGeoplace.OnValueChanged.AddListener(x => ShowSaveButtons());
            feldInstitutName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldDirection.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldSpecialization.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerStartDate.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerEndDate.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        private void InitializeFields()
        {
            dropdownInstitutType.ClearOptions();
            dropdownInstitutType.AddOptions(EnumConverterStrategy<InstitutionTypeEnum>.Data.Values.ToList());

            dropdownDegree.ClearOptions();
            dropdownDegree.AddOptions(EnumConverterStrategy<EducationDegreeEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            // Обновление поля для ввода типа учебного заведения
            string institutTypeStr = _pageData.Value.InstitutionType.HasValue
                ? EnumConverterStrategy<InstitutionTypeEnum>.ToString(_pageData.Value.InstitutionType.Value)
                : null;
            dropdownInstitutType.value = dropdownInstitutType.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(institutTypeStr);

            // Обновление поля для ввода названия учебного заведения
            feldInstitutName.Text = _pageData.Value.InstitutionName;

            // Обновление поля для ввода местоположения учебного заведения
            sectionGeoplace.UpdateData(_pageData.Value.InstitutionAddress);

            // Обновление поля для ввода направления обучения
            fieldDirection.Text = _pageData.Value.Direction;

            // Обновление поля для ввода специализации
            fieldSpecialization.Text = _pageData.Value.Specialization;

            // Обновление поля для ввода степени образования
            string degreeStr = _pageData.Value.Degree.HasValue
                ? EnumConverterStrategy<EducationDegreeEnum>.ToString(_pageData.Value.Degree.Value)
                : null;
            dropdownDegree.value = dropdownDegree.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(degreeStr);

            // Обновление полей для ввода периода обучения
            datepickerStartDate.Date = _pageData.Value.StartDate;
            datepickerEndDate.Date = _pageData.Value.EndDate;
        }

        public override void ResetFields()
        {
            sectionGeoplace.ResetFields();

            dropdownInstitutType.value = -1;
            dropdownDegree.value = -1;

            feldInstitutName.Text = string.Empty;
            fieldDirection.Text = string.Empty;
            fieldSpecialization.Text = string.Empty;
            datepickerStartDate.Date = null;
            datepickerEndDate.Date = null;
        }

        protected override Education CollectData()
        {
            var data = new Education()
            {
                PersonId = this.PersonId,

                InstitutionType = dropdownInstitutType.value == -1
                ? null
                : EnumConverterStrategy<InstitutionTypeEnum>.ToEnum(dropdownInstitutType.options[dropdownInstitutType.value].text),

                InstitutionName = feldInstitutName.Text,
                InstitutionAddress = sectionGeoplace.CollectData(),
                Direction = fieldDirection.Text,
                Specialization = fieldSpecialization.Text,

                Degree = dropdownDegree.value == -1
                ? null
                : EnumConverterStrategy<EducationDegreeEnum>.ToEnum(dropdownDegree.options[dropdownDegree.value].text),

                StartDate = datepickerStartDate.Date,
                EndDate = datepickerEndDate.Date,
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
                   feldInstitutName.TextIsValid &&
                   fieldDirection.TextIsValid &&
                   fieldSpecialization.TextIsValid &&
                   datepickerStartDate.IsContainValidDate &&
                   datepickerEndDate.IsContainValidDate;
        }
    }
}
