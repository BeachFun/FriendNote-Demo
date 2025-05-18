using FriendNote.Domain.DTO;
using RUI;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonWorkPosition : EntityEditPage<WorkPosition>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private InputField fieldCompanyName;
        [SerializeField] private InputField fieldPostionName;
        [SerializeField] private InputField fieldDuties;
        [SerializeField] private InputField fieldSalary;
        [SerializeField] private UI_DatePicker datepickerStart;
        [SerializeField] private UI_DatePicker datepickerEnd;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            // Подписка на события изменения данных в полях ввода
            fieldCompanyName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldPostionName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldDuties.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldSalary.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerStart.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerEnd.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            fieldCompanyName.Text = _pageData.Value.CompanyName;
            fieldPostionName.Text = _pageData.Value.PositionName;
            fieldDuties.Text = _pageData.Value.Duties;
            fieldSalary.Text = _pageData.Value.Salary;
            datepickerStart.Date = _pageData.Value.StartDate;
            datepickerEnd.Date = _pageData.Value.EndDate;
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
                data.Id = _pageData.Value.Id;
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
