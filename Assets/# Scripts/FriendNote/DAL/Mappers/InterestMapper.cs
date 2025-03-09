using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class InterestMapper : IMapper<InterestORM, Interest>
    {
        public Interest ToDTO(InterestORM source)
        {
            return new Interest
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Category = source.Category,
                Name = source.Name,
                Description = source.Description,
                Level = source.Level
            };
        }

        public InterestORM ToORM(Interest source)
        {
            return new InterestORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Category = source.Category,
                Name = source.Name,
                Description = source.Description,
                Level = source.Level
            };
        }
    }


}
