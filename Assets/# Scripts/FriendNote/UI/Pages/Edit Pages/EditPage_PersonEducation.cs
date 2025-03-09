using System.Linq;
using FriendNote.Core.Converters;
using FriendNote.Core.DTO;
using FriendNote.Core.Enums;
using Observer;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonEducation : RelatedInfoEditPage<Education>
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

        private void Awake()
        {
            InitializeFields();

            // Подписка на события изменения данных в полях ввода
            dropdownInstitutType.onValueChanged.AddListener(OnValueChanged);
            dropdownDegree.onValueChanged.AddListener(OnValueChanged);
            sectionGeoplace.OnValueChanged.AddListener(OnValueChanged);
            feldInstitutName.onValueChanged.AddListener(OnValueChanged);
            fieldDirection.onValueChanged.AddListener(OnValueChanged);
            fieldSpecialization.onValueChanged.AddListener(OnValueChanged);
            datepickerStartDate.onValueChanged.AddListener(OnValueChanged);
            datepickerEndDate.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            // Отписка от событий изменения данных в полях ввода
            dropdownInstitutType.onValueChanged.RemoveListener(OnValueChanged);
            dropdownDegree.onValueChanged.RemoveListener(OnValueChanged);
            sectionGeoplace.OnValueChanged.RemoveListener(OnValueChanged);
            feldInstitutName.onValueChanged.RemoveListener(OnValueChanged);
            fieldDirection.onValueChanged.RemoveListener(OnValueChanged);
            fieldSpecialization.onValueChanged.RemoveListener(OnValueChanged);
            datepickerStartDate.onValueChanged.RemoveListener(OnValueChanged);
            datepickerEndDate.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void InitializeFields()
        {
            dropdownInstitutType.ClearOptions();
            dropdownInstitutType.AddOptions(EnumConverterStrategy<InstitutionTypeEnum>.Data.Values.ToList());

            dropdownDegree.ClearOptions();
            dropdownDegree.AddOptions(EnumConverterStrategy<EducationDegreeEnum>.Data.Values.ToList());
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_EDUCATION_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            // Обновление поля для ввода типа учебного заведения
            string institutTypeStr = _pageData.InstitutionType.HasValue
                ? EnumConverterStrategy<InstitutionTypeEnum>.ToString(_pageData.InstitutionType.Value)
                : null;
            dropdownInstitutType.value = dropdownInstitutType.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(institutTypeStr);

            // Обновление поля для ввода названия учебного заведения
            feldInstitutName.Text = _pageData.InstitutionName;

            // Обновление поля для ввода местоположения учебного заведения
            sectionGeoplace.DataUpdate(_pageData.InstitutionAddress);

            // Обновление поля для ввода направления обучения
            fieldDirection.Text = _pageData.Direction;

            // Обновление поля для ввода специализации
            fieldSpecialization.Text = _pageData.Specialization;

            // Обновление поля для ввода степени образования
            string degreeStr = _pageData.Degree.HasValue
                ? EnumConverterStrategy<EducationDegreeEnum>.ToString(_pageData.Degree.Value)
                : null;
            dropdownDegree.value = dropdownDegree.options
                .Select(x => x.text)
                .ToList()
                .IndexOf(degreeStr);

            // Обновление полей для ввода периода обучения
            datepickerStartDate.Date = _pageData.StartDate;
            datepickerEndDate.Date = _pageData.EndDate;
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
                data.Id = _pageData.Id;
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
