using FriendNote.Domain.DTO;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonGoal : EntityEditPage<Goal>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private RUI.InputField fieldGoalName;
        [SerializeField] private RUI.InputField fieldGoalDesc;
        [SerializeField] private Toggle toggleIsTarget;
        [SerializeField] private UI_DatePicker datepickerTargetDate;


        #region [Методы] Управление жизненным циклом

        protected override void Awake()
        {
            base.Awake();

            fieldGoalName.onValueChanged.AddListener(x => ShowSaveButtons());
            fieldGoalDesc.onValueChanged.AddListener(x => ShowSaveButtons());
            toggleIsTarget.onValueChanged.AddListener(x => ShowSaveButtons());
            datepickerTargetDate.onValueChanged.AddListener(x => ShowSaveButtons());
        }

        #endregion


        public override void Refresh()
        {
            ResetFields();

            if (_pageData.Value is null) return;

            fieldGoalName.Text = _pageData.Value.Name;
            fieldGoalDesc.Text = _pageData.Value.Description;
            toggleIsTarget.isOn = _pageData.Value.IsTarget;
            datepickerTargetDate.Date = _pageData.Value.TargetDate;
        }

        public override void ResetFields()
        {
            fieldGoalName.Text = string.Empty;
            fieldGoalDesc.Text = string.Empty;
            toggleIsTarget.isOn = false;
            datepickerTargetDate.Date = null;
        }

        protected override Goal CollectData()
        {
            var data = new Goal()
            {
                PersonId = this.PersonId,
                Name = fieldGoalName.Text,
                Description = fieldGoalDesc.Text,
                IsTarget = toggleIsTarget.isOn,
                TargetDate = datepickerTargetDate.Date
            };

            if (EditMode == EditModeEnum.Updating)
            {
                data.Id = _pageData.Value.Id;
            }

            return data;
        }

        protected override bool CheckFillValidation()
        {
            return fieldGoalName.TextIsValid &&
                fieldGoalDesc.TextIsValid &&
                datepickerTargetDate.IsContainValidDate;
        }
    }
}
