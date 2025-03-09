using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для SkillLevelEnum
    /// </summary>
    public class SkillLevelConverter : EnumConverter<SkillLevelEnum>
    {
        public SkillLevelConverter()
        {
            Data = new()
            {
                { SkillLevelEnum.Elementary, "Элементарный" },
                { SkillLevelEnum.Low, "Низкий" },
                { SkillLevelEnum.Medium, "Средний" },
                { SkillLevelEnum.High, "Высокий" },
                { SkillLevelEnum.Expert, "Эксперт" },
                { SkillLevelEnum.Master, "Мастер" },
                { SkillLevelEnum.Genius, "Гений" }
            };
        }
    }
}
