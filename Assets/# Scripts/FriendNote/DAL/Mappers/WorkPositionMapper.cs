using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class WorkPositionMapper : IMapper<WorkPositionORM, WorkPosition>
    {
        public WorkPosition ToDTO(WorkPositionORM source)
        {
            return new WorkPosition
            {
                Id = source.Id,
                PersonId = source.PersonId,
                CompanyName = source.CompanyName,
                PositionName = source.PositionName,
                Duties = source.Duties,
                Salary = source.Salary,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };
        }

        public WorkPositionORM ToORM(WorkPosition source)
        {
            return new WorkPositionORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                CompanyName = source.CompanyName,
                PositionName = source.PositionName,
                Duties = source.Duties,
                Salary = source.Salary,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };
        }
    }
}
