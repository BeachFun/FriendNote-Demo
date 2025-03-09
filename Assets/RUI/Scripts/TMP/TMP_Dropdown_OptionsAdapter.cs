using TMPro;
using UnityEngine;

namespace RUI
{
    [RequireComponent(typeof(TMP_Dropdown))]

    [AddComponentMenu("RUI/TMP_Dropdown - Option Adapter")]
    public class TMP_Dropdown_OptionsAdapter : MonoBehaviour // TODO: в разработке
    {
        //[SerializeField] private Dictionary<string, string>

        private TMP_Dropdown targetDropdown;


        private void Awake()
        {
            targetDropdown = GetComponent<TMP_Dropdown>();
        }


        private void OnSelect(string str)
        {
            //ChooseBorderColor(validator.State);
        }

        private void OnDeselect(string str)
        {
            //ChooseBorderColor(validator.State);
        }

    }
}
