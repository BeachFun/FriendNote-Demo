using FriendNote.Core.Enums;

namespace FriendNote.Core.DTO
{
    public class Interest : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public InterestLevelEnum? Level { get; set; }
    }
}
