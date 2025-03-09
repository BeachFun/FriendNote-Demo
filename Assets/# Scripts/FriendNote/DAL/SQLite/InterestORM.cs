using SQLite;
using FriendNote.Core.Enums;

namespace FriendNote.Data.Tables
{
    [Table("Interest")]
    public sealed class InterestORM : ITable, IPersonTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("interest_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("interest_category")]
        public string Category { get; set; }

        [Column("interest_name")]
        public string Name { get; set; }

        [Column("interest_description")]
        public string Description { get; set; }

        [Column("interest_level")]
        public InterestLevelEnum? Level { get; set; }
    }
}
