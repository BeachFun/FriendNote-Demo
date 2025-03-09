using System;

namespace FriendNote.Core.DTO
{
    public class WorkPosition : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }
        public string Duties { get; set; }
        public string Salary { get; set; }
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
