using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class GoalMapper : IMapper<GoalORM, Goal>
    {
        public Goal ToDTO(GoalORM source)
        {
            return new Goal
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Name = source.Name,
                Description = source.Description,
                IsTarget = source.IsTarget,
                TargetDate = source.TargetDate
            };
        }

        public GoalORM ToORM(Goal source)
        {
            return new GoalORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Name = source.Name,
                Description = source.Description,
                IsTarget = source.IsTarget,
                TargetDate = source.TargetDate
            };
        }
    }
}
