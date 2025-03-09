using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class AddressMapper : IMapper<AddressORM, Address>
    {
        public AddressORM ToORM(Address destination)
        {
            return new AddressORM()
            {
                Id = destination.Id,
                Country = destination.Country,
                State = destination.State,
                City = destination.City,
                Street = destination.Street,
                House = destination.House,
                Apartment = destination.Apartment,
                PostalCode = destination.PostalCode
            };
        }

        public Address ToDTO(AddressORM source)
        {
            return new Address()
            {
                Id = source.Id,
                Country = source.Country,
                State = source.State,
                City = source.City,
                Street = source.Street,
                House = source.House,
                Apartment = source.Apartment,
                PostalCode = source.PostalCode
            };
        }
    }
}
