using System;
using FriendNote.Core.Enums;

namespace FriendNote.Domain.DTO
{
    public class Residence : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; }
        public Address Address { get; set; }
        public CityPartEnum? PartOfCity { get; set; }
        public ApartmentsEnum? ApartmentType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public string GetPeriod()
        {
            if (StartDate.IsDefault() && EndDate.IsDefault()) return null;

            if (StartDate.IsDefault()) return $"... - {EndDate?.ToString("dd.MM.yyyy")}";
            if (EndDate.IsDefault()) return $"{StartDate?.ToString("dd.MM.yyyy")} - н.в.";

            return $"{StartDate?.ToString("dd.MM.yyyy")} - {EndDate?.ToString("dd.MM.yyyy")}";
        }
    }
}
