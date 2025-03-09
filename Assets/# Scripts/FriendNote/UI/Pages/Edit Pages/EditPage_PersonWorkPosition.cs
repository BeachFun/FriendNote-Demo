using FriendNote.Core.DTO;
using Observer;
using RUI;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonWorkPosition : RelatedInfoEditPage<WorkPosition>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCompanyName;
        [SerializeField] private InputField fieldPostionName;
        [SerializeField] private InputField fieldDuties;
        [SerializeField] private InputField fieldSalary;
        [SerializeField] private UI_DatePicker datepickerStart;
        [SerializeField] private UI_DatePicker datepickerEnd;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            // Подписка на события изменения данных в полях ввода
            fieldCompanyName.onValueChanged.AddListener(OnValueChanged);
            fieldPostionName.onValueChanged.AddListener(OnValueChanged);
            fieldDuties.onValueChanged.AddListener(OnValueChanged);
            fieldSalary.onValueChanged.AddListener(OnValueChanged);
            datepickerStart.onValueChanged.AddListener(OnValueChanged);
            datepickerEnd.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            // Отписка от событий изменения данных в полях ввода
            fieldCompanyName.onValueChanged.RemoveListener(OnValueChanged);
            fieldPostionName.onValueChanged.RemoveListener(OnValueChanged);
            fieldDuties.onValueChanged.RemoveListener(OnValueChanged);
            fieldSalary.onValueChanged.RemoveListener(OnValueChanged);
            datepickerStart.onValueChanged.RemoveListener(OnValueChanged);
            datepickerEnd.onValueChanged.RemoveListener(OnValueChanged);
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_WORK_POSITION_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            fieldCompanyName.Text = _pageData.CompanyName;
            fieldPostionName.Text = _pageData.PositionName;
            fieldDuties.Text = _pageData.Duties;
            fieldSalary.Text = _pageData.Salary;
            datepickerStart.Date = _pageData.StartDate;
            datepickerEnd.Date = _pageData.EndDate;
        }

        public override void ResetFields()
        {
            fieldCompanyName.Text = string.Empty;
            fieldPostionName.Text = string.Empty;
            fieldDuties.Text = string.Empty;
            fieldSalary.Text = string.Empty;
            datepickerStart.Date = null;
            datepickerEnd.Date = null;
        }

        protected override WorkPosition CollectData()
        {
            var data = new WorkPosition()
            {
                PersonId = this.PersonId,
                CompanyName = fieldCompanyName.Text,
                PositionName = fieldPostionName.Text,
                Duties = fieldDuties.Text,
                Salary = fieldSalary.Text,
                StartDate = datepickerStart.Date,
                EndDate = datepickerEnd.Date
            };

            if (EditMode == EditModeEnum.Updating)
            {
                data.Id = _pageData.Id;
            }

            return data;
        }

        protected override bool CheckFillValidation()
        {
            return fieldCompanyName.TextIsValid &&
                fieldPostionName.TextIsValid &&
                fieldDuties.TextIsValid &&
                fieldSalary.TextIsValid &&
                datepickerStart.IsContainValidDate &&
                datepickerEnd.IsContainValidDate;
        }
    }
}
