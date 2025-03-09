namespace Bitsplash.DatePicker
{
    interface IDatePickerPrivate
    {
        void RaiseClick(int childIndex);
        void RaiseStartSelection(int childIndex);
        void RaiseSelectionEnter(int childIndex, int fromChildIndex);
        void RaiseSelectionExit(int childIndex, int fromChildIndex);
        void EndSelection();
    }
}
