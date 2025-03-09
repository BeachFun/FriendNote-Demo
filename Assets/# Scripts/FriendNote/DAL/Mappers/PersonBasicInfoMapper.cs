using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class PersonBasicInfoMapper : IMapper<PersonORM, PersonBasicInfo>
    {
        public PersonBasicInfo ToDTO(PersonORM source)
        {
            return new PersonBasicInfo
            {
                Id = source.Id,
                Nickname = source.Nickname,
                Name = source.Name,
                Surname = source.Surname,
                Patronymic = source.Patronymic,
                Gender = source.Gender,
                Birthdate = source.Birthdate,
                RelationshipType = source.RelationshipType,
                dateEditRelationshipType = source.dateEditRelationshipType,
                Acquaintance = source.Acquaintance,
                Notation = source.Notation
            };
        }

        public PersonORM ToORM(PersonBasicInfo source)
        {
            return new PersonORM
            {
                Id = source.Id,
                Nickname = source.Nickname,
                Name = source.Name,
                Surname = source.Surname,
                Patronymic = source.Patronymic,
                Gender = source.Gender,
                Birthdate = source.Birthdate,
                RelationshipType = source.RelationshipType,
                dateEditRelationshipType = source.dateEditRelationshipType,
                Acquaintance = source.Acquaintance,
                Notation = source.Notation
            };
        }
    }
}
