using System;
using SQLite;
using FriendNote.Core.Enums;

namespace FriendNote.Data.Tables
{
    [Table("Residence")]
    public sealed class ResidenceORM : ITable, IPersonTableLink, IAddressTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("residence_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [NotNull, Column("address_id")]
        public int? AddressId { get; set; } = -1; // link of Address table

        [Column("part_of_city")]
        public CityPartEnum? CityPart { get; set; }

        [Column("apartment_type")]
        public ApartmentsEnum? Apaerments { get; set; }

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
