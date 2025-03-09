using System;

namespace Bitsplash.DatePicker
{
    interface IDatePickerCollectionPrivate
    {
        bool Changed { get; set; }
        bool AllowEmpty { get; set; }
        event Action SelectionModified;
    }
}
