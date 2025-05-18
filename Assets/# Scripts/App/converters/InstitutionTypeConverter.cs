using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь для InstitutionTypeEnum
    /// </summary>
    public class InstitutionTypeConverter : EnumConverter<InstitutionTypeEnum>
    {
        public InstitutionTypeConverter()
        {
            Data = new()
            {
                { InstitutionTypeEnum.Kindergarten, "Детский сад" },
                { InstitutionTypeEnum.School, "Школа" },
                { InstitutionTypeEnum.Liceum, "Лицей" },
                { InstitutionTypeEnum.Gymnasium, "Гимназия" },
                { InstitutionTypeEnum.College, "Колледж" },
                { InstitutionTypeEnum.TechnicalSchool, "Техникум" },
                { InstitutionTypeEnum.University, "Университет" },
                { InstitutionTypeEnum.Academy, "Академия" },
                { InstitutionTypeEnum.Institute, "Институт" },
                { InstitutionTypeEnum.ProfessionalDevelopmentCourses, "Курсы повышения квалификации" },
                { InstitutionTypeEnum.TrainingCenter, "Учебный центр" },
                { InstitutionTypeEnum.Other, "Другое" }
            };
        }
    }
}