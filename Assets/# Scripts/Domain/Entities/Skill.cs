using FriendNote.Core.Enums;

namespace FriendNote.Domain.DTO
{
    public class Skill : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillLevelEnum? Level { get; set; }
        public string UsageScope { get; set; }
    }
}
