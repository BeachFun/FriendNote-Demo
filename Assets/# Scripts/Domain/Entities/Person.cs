using System;
using FriendNote.Core.Enums;

namespace FriendNote.Domain.DTO
{
    /// <summary>
    /// Базовая информация о человеке
    /// </summary>
    public class Person : EntityBase
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public RelationshipTypeEnum? RelationshipType { get; set; }
        public DateTime? dateEditRelationshipType { get; set; }

        /// <summary>
        /// Знакомство
        /// </summary>
        public string Acquaintance { get; set; }
        public string Notation { get; set; }
    }
}
