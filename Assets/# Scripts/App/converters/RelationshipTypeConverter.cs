using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для RelationshipTypeEnum
    /// </summary>
    public class RelationshipTypeConverter : EnumConverter<RelationshipTypeEnum>
    {
        public RelationshipTypeConverter()
        {
            Data = new()
            {
                { RelationshipTypeEnum.Family, "Родственные" },
                { RelationshipTypeEnum.Friends, "Дружеские" },
                { RelationshipTypeEnum.Comradeship, "Товарищеские" },
                { RelationshipTypeEnum.Acquaintances, "Знакомые" },
                { RelationshipTypeEnum.Professional, "Профессиональные" },
                { RelationshipTypeEnum.Romantic, "Романтические" },
                { RelationshipTypeEnum.Conflict, "Конфликтные" },
                { RelationshipTypeEnum.Undefined, "Неопределённые" },
            };
        }
    }
}
