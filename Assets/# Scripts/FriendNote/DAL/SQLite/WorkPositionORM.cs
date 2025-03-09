using System;
using SQLite;

namespace FriendNote.Data.Tables
{
    [Table("Career")]
    public sealed class WorkPositionORM : ITable, IPersonTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("career_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("company_name")]
        public string CompanyName { get; set; }

        [Column("work_position")]
        public string PositionName { get; set; }

        [Column("work_duties")]
        public string Duties { get; set; }

        [Column("salary")]
        public string Salary { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }


        public string GetPeriod()
        {
            if (StartDate.IsDefault() && EndDate.IsDefault()) return null;

            if (StartDate.IsDefault()) return $"... - {EndDate?.ToString("dd.MM.yyyy")}";
            if (EndDate.IsDefault()) return $"{StartDate?.ToString("dd.MM.yyyy")} - ...";

            return $"{StartDate?.ToString("dd.MM.yyyy")} - {EndDate?.ToString("dd.MM.yyyy")}";
        }
    }
}
