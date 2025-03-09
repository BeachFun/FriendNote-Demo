using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class EducationMapper : IMapper<EducationORM, AddressORM, Education>
    {
        private readonly AddressMapper _addressMapper = new();

        public Education ToDTO(EducationORM source, AddressORM institutionAddress)
        {
            return new Education
            {
                Id = source.Id,
                PersonId = source.PersonId,
                InstitutionType = source.InstitutionType,
                InstitutionName = source.InstitutionName,
                InstitutionAddress = institutionAddress != null ? _addressMapper.ToDTO(institutionAddress) : null,
                Direction = source.Direction,
                Specialization = source.Specialization,
                Degree = source.Degree,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };
        }

        public (EducationORM, AddressORM) ToORM(Education source)
        {
            return (new EducationORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                InstitutionType = source.InstitutionType,
                InstitutionName = source.InstitutionName,
                AddressId = source.InstitutionAddress?.Id,
                Direction = source.Direction,
                Specialization = source.Specialization,
                Degree = source.Degree,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            }, _addressMapper.ToORM(source.InstitutionAddress));
        }
    }
}
