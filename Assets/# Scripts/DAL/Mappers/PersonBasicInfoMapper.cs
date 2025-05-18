using FriendNote.Data.Tables;
using FriendNote.Domain.DTO;

namespace FriendNote.Data.Mappers
{
    public class PersonBasicInfoMapper : IMapper<PersonORM, Person>
    {
        public Person ToDTO(PersonORM source)
        {
            return new Person
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

        public PersonORM ToORM(Person source)
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
