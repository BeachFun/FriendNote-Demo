using FriendNote.Core.DTO;
using Observer;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Pages
{
    public class EditPage_PersonGoal : RelatedInfoEditPage<Goal>
    {
        [Header("Required Page Fields References")]
        [SerializeField] private RUI.InputField fieldGoalName;
        [SerializeField] private RUI.InputField fieldGoalDesc;
        [SerializeField] private Toggle toggleIsTarget;
        [SerializeField] private UI_DatePicker datepickerTargetDate;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            fieldGoalName.onValueChanged.AddListener(OnValueChanged);
            fieldGoalDesc.onValueChanged.AddListener(OnValueChanged);
            toggleIsTarget.onValueChanged.AddListener(OnValueChanged);
            datepickerTargetDate.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            fieldGoalName.onValueChanged.RemoveListener(OnValueChanged);
            fieldGoalDesc.onValueChanged.RemoveListener(OnValueChanged);
            toggleIsTarget.onValueChanged.RemoveListener(OnValueChanged);
            datepickerTargetDate.onValueChanged.RemoveListener(OnValueChanged);
        }

        #endregion


        public override void Close()
        {
            base.Close();

            Messenger.Broadcast(Notices.UINotice.EDIT_PAGE_PERSON_GOAL_CLOSED);
        }


        public override void PageUpdate()
        {
            ResetFields();

            fieldGoalName.Text = _pageData.Name;
            fieldGoalDesc.Text = _pageData.Description;
            toggleIsTarget.isOn = _pageData.IsTarget;
            datepickerTargetDate.Date = _pageData.TargetDate;
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
                data.Id = _pageData.Id;
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
