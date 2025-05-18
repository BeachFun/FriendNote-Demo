using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для InterestLevelEnum
    /// </summary>
    public class InterestLevelConverter : EnumConverter<InterestLevelEnum>
    {
        public InterestLevelConverter()
        {
            Data = new()
            {
                { InterestLevelEnum.Zero, "На словах | Нулевая увлеченность" },
                { InterestLevelEnum.Weak, "Слабый | Пассивный интерес" },
                { InterestLevelEnum.Moderate, "Нормальный | Активный интерес" },
                { InterestLevelEnum.High, "Высокий | Глубокий интерес" },
                { InterestLevelEnum.Extreme, "Фанатизм | крайняя увлеченность" }
            };
        }
    }
}
