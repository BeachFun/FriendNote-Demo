namespace FriendNote
{
    /// <summary>
    /// Статусы поля ввода
    /// </summary>
    public enum FieldStatusEnum
    {
        Sleeping, // до того как пользователь нажал на поле
        Selected, // после того как пользователь нажал на поле
        EditIncorrected, // после неверных введенных данных
        EditCorrected // после верно введенных данных
    }
}
