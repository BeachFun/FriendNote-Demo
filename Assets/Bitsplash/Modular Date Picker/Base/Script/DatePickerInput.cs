using UnityEngine;

namespace Bitsplash.DatePicker
{
    public abstract class DatePickerInput : MonoBehaviour
    {
        public abstract MultipleSelectionInputValue MultipleSelectionValue { get; }
    }
}
