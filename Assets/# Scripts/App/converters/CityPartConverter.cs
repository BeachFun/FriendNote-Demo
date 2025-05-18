using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для CityPartEnum
    /// </summary>
    public class CityPartConverter : EnumConverter<CityPartEnum>
    {
        public CityPartConverter()
        {
            Data = new()
            {
                { CityPartEnum.PoorArea, "Окраина" },
                { CityPartEnum.WealthyArea, "Спальный район" },
                { CityPartEnum.RichArea, "Центральный район" },
            };
        }
    }
}
