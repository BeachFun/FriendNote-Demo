using UnityEngine;

namespace Bitsplash.DatePicker
{
    public class DatePickerBackgroundSetting : MonoBehaviour, IDatePickerSettingsItem
    {
        [SerializeField]
        [HideInInspector]
        private bool isOpen;

        public string EditorTitle { get { return gameObject.name; } }

        public int Order { get { return 1; } }
    }
}
