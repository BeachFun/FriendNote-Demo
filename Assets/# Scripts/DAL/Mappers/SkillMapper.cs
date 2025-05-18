using FriendNote.Data.Tables;
using FriendNote.Domain.DTO;

namespace FriendNote.Data.Mappers
{
    public class SkillMapper : IMapper<SkillORM, Skill>
    {
        public Skill ToDTO(SkillORM source)
        {
            return new Skill
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Category = source.Category,
                Name = source.Name,
                Description = source.Description,
                Level = source.Level,
                UsageScope = source.UsageScope
            };
        }

        public SkillORM ToORM(Skill source)
        {
            return new SkillORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Category = source.Category,
                Name = source.Name,
                Description = source.Description,
                Level = source.Level,
                UsageScope = source.UsageScope
            };
        }
    }
}
