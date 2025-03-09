using System;
using SQLite;
using FriendNote.Core.Enums;

namespace FriendNote.Data.Tables
{
    [Table("Person")]
    public sealed class PersonORM : ITable
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("person_id")]
        public int? Id { get; set; } = -1;

        [Column("nickname")]
        public string Nickname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("gender")]
        public GenderEnum? Gender { get; set; }

        [Column("birthdate")]
        public DateTime? Birthdate { get; set; }

        [Column("relationship type")]
        public RelationshipTypeEnum? RelationshipType { get; set; }

        [Column("datetimeEdit_RT")]
        public DateTime? dateEditRelationshipType { get; set; }

        [Column("acquaintance")]
        public string Acquaintance { get; set; }

        [Column("notation")]
        public string Notation { get; set; }
    }
}
