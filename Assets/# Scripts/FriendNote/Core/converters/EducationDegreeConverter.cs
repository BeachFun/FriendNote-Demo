using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для EducationDegreeEnum
    /// </summary>
    public class EducationDegreeConverter : EnumConverter<EducationDegreeEnum>
    {
        public EducationDegreeConverter()
        {
            Data = new()
            {
                { EducationDegreeEnum.PrePrimary, "Дошкольное" },
                { EducationDegreeEnum.Primary, "Начальное | 4 кл" },
                { EducationDegreeEnum.IncompleteBasicGeneral, "Неполное основное общее" },
                { EducationDegreeEnum.BasicGeneral, "Основное общее | 9 кл" },
                { EducationDegreeEnum.SecondaryGeneral, "Среднее общее | 11 кл" },
                { EducationDegreeEnum.SecondaryVocational, "Среднее профессиональное образование" },
                { EducationDegreeEnum.IncompleteHigher, "Неполное высшее" },
                { EducationDegreeEnum.Bachelor, "Высшее | Бакалавр" },
                { EducationDegreeEnum.Specialist, "Высшее | Специалист" },
                { EducationDegreeEnum.Master, "Высшее | Магистр" },
                { EducationDegreeEnum.Postgraduate, "Аспирант | Кандидат доктора наук" },
                { EducationDegreeEnum.Doctoral, "Докторант | Доктор наук" }
            };
        }
    }
}
