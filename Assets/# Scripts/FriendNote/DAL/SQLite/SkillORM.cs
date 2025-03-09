using SQLite;
using FriendNote.Core.Enums;

namespace FriendNote.Data.Tables
{
    [Table("Skill")]
    public sealed class SkillORM : ITable, IPersonTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("skill_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("skill_category")]
        public string Category { get; set; }

        [Column("skill_name")]
        public string Name { get; set; }

        [Column("skill_description")]
        public string Description { get; set; }

        [Column("skill_level")]
        public SkillLevelEnum? Level { get; set; } // SkillLevelEnum

        [Column("usage_scope")]
        public string UsageScope { get; set; }
    }
}
