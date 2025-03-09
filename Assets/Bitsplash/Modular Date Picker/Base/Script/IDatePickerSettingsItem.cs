using System;

namespace Bitsplash.DatePicker
{
    public interface IDatePickerSettingsItem
    {
        String EditorTitle { get; }
        int Order { get; }
    }
}
