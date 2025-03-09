using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class ResidenceMapper : IMapper<ResidenceORM, AddressORM, Residence>
    {
        private readonly AddressMapper _addressMapper = new();

        public Residence ToDTO(ResidenceORM source, AddressORM address)
        {
            return new Residence
            {
                Id = source.Id,
                PersonId = source.PersonId,
                Address = address != null ? _addressMapper.ToDTO(address) : null,
                PartOfCity = source.CityPart,
                ApartmentType = source.Apaerments,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };
        }

        public (ResidenceORM, AddressORM) ToORM(Residence source)
        {
            return (new ResidenceORM
            {
                Id = source.Id,
                PersonId = source.PersonId,
                AddressId = source.Address?.Id, // Если Geoplace имеет Id, передаем его в geoplace_id
                CityPart = source.PartOfCity,
                Apaerments = source.ApartmentType,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            }, _addressMapper.ToORM(source.Address));
        }
    }
}
