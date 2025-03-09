using FriendNote.Core.DTO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI.Cards
{
    public class UI_PersonCard_Goal : UI_PersonCard<Goal>
    {
        [Header("References")]
        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text textDescription;
        [SerializeField] private Toggle toggleIsTargetComplete;
        [SerializeField] private GameObject panelTargetDate;
        [SerializeField] private TMP_Text textTargetDate;


        public override void UpdateData(Goal data)
        {
            base.UpdateData(data);

            textName.text = string.IsNullOrEmpty(data.Name)
                ? _emptyField
                : data.Name;

            textDescription.text = string.IsNullOrEmpty(data.Description)
                ? _emptyField
                : data.Description;

            toggleIsTargetComplete.isOn = data.IsTarget;
            panelTargetDate.SetActive(data.IsTarget); // показываем текст с датой, если цель выполнена

            textTargetDate.text = data.TargetDate.HasValue
                ? data.TargetDate.Value.ToString("dd.MM.yyyy")
                : _emptyField;

            toggleIsTargetComplete.onValueChanged.AddListener(AutoSave); // располагается здесь, чтобы не происходило автосохранения при создании карточки
        }

        public void AutoSave(bool isOn)
        {
            _data.IsTarget = isOn;
            panelTargetDate.SetActive(isOn);

            Services.EntityData.SaveEntity<Goal>(_data);
        }
    }
}
