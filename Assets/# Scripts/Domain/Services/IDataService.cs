using FriendNote.Core;
using FriendNote.Domain.DTO;

namespace FriendNote.Domain
{
    public interface IDataService : IService
    {
        PersonContacts LoadPersonContacts(int personId);
        PersonProfile LoadPersonProfile(int personId);
        bool RemovePersonProfile(PersonProfile personProfile);
        bool SavePersonProfile(PersonProfile personProfile);
    }
}