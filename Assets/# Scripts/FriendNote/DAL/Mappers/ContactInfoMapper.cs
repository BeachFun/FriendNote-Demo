using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public class ContactInfoMapper : IMapper<ContactInfoORM, ContactInfo>
    {
        public ContactInfo ToDTO(ContactInfoORM source)
        {
            return new ContactInfo()
            {
                Id = source.Id,
                ContactData = source.ContactData,
                ContactSubtype = source.ContactSubtype,
                ContactType = source.ContactType,
                PersonId = source.PersonId
            };
        }

        public ContactInfoORM ToORM(ContactInfo source)
        {
            return new ContactInfoORM()
            {
                Id = source.Id,
                ContactData = source.ContactData,
                ContactSubtype = source.ContactSubtype,
                ContactType = source.ContactType,
                PersonId = source.PersonId
            };
        }
    }
}
