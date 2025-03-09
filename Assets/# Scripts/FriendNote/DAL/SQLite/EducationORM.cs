using System;
using FriendNote.Core.Enums;
using SQLite;

namespace FriendNote.Data.Tables
{
    [Table("Education")]
    public sealed class EducationORM : ITable, IPersonTableLink, IAddressTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("education_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("institution_type")]
        public InstitutionTypeEnum? InstitutionType { get; set; }

        [Column("institution_name")]
        public string InstitutionName { get; set; }

        [Column("address_id")]
        public int? AddressId { get; set; } = -1;

        [Column("direction")]
        public string Direction { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("degree")]
        public EducationDegreeEnum? Degree { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }
    }
}
