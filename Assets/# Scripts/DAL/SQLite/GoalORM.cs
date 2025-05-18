using System;
using SQLite;

namespace FriendNote.Data.Tables
{
    [Table("Goal")]
    public sealed class GoalORM : ITable, IPersonTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("goal_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("goal_name")]
        public string Name { get; set; }

        [Column("goal_description")]
        public string Description { get; set; }

        [Column("is_target")]
        public bool IsTarget { get; set; }

        [Column("target_date")]
        public DateTime? TargetDate { get; set; }
    }
}
