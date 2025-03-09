using FriendNote.Core.DTO;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_WorkPosition : UI_PersonCard<WorkPosition>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textPositionName;
        [SerializeField] private TMP_Text textCompanyName;
        [SerializeField] private TMP_Text textDuties;
        [SerializeField] private TMP_Text textSalary;
        [SerializeField] private TMP_Text textWorkPeriod;


        public override void UpdateData(WorkPosition data)
        {
            base.UpdateData(data);

            textPositionName.text = string.IsNullOrEmpty(data.PositionName)
                ? _emptyField
                : data.PositionName;

            textCompanyName.text = string.IsNullOrEmpty(data.CompanyName)
                ? _emptyField
                : data.CompanyName;

            textDuties.text = string.IsNullOrEmpty(data.Duties)
                ? _emptyField
                : data.Duties;

            textSalary.text = string.IsNullOrEmpty(data.Salary)
                ? _emptyField
                : data.Salary;

            textWorkPeriod.text = string.IsNullOrEmpty(data.GetPeriod())
                ? _emptyField
                : data.GetPeriod();
        }
    }
}
