using System;

namespace FriendNote.Core.DTO
{
    public class Goal : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTarget { get; set; }
        public DateTime? TargetDate { get; set; }
    }
}
