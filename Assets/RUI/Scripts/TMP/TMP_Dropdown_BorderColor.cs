using TMPro;
using UnityEngine;

namespace RUI
{
    [RequireComponent(typeof(TMP_Dropdown))]

    [AddComponentMenu("RUI/TMP_Dropdown - BorderColor")]
    public class TMP_Dropdown_BorderColor : MonoBehaviour
    {
        [Header("TargetGraphic colors")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color chooseColor = Color.green;

        private TMP_Dropdown targetDropdown;


        private void Awake()
        {
            targetDropdown = GetComponent<TMP_Dropdown>();
            targetDropdown.onValueChanged.AddListener(ChooseBorderColor);
        }


        private void OnSelect(string str)
        {
            //ChooseBorderColor(validator.State);
        }

        private void OnDeselect(string str)
        {
            //ChooseBorderColor(validator.State);
        }

        private void ChooseBorderColor(int optionIndex)
        {
            if (-1 < optionIndex /*&& i < dropdown.options.Count*/) // unity значение за пределами массива считает последний элемент
            {
                targetDropdown.targetGraphic.color = chooseColor;
            }
            else
            {
                targetDropdown.targetGraphic.color = normalColor;
            }
        }
    }
}
