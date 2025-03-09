using System.Collections.Generic;

namespace FriendNote.Core.DTO
{
    /// <summary>
    /// Контактная информация
    /// </summary>
    public class PersonContacts
    {
        public int? PersonId { get; set; }
        public List<ContactInfo> PhoneNumbers { get; set; } = new();
        public List<ContactInfo> SocialMedia { get; set; } = new();
        public List<ContactInfo> Emails { get; set; } = new();
        public List<ContactInfo> Others { get; set; } = new();

        public PersonContacts() { }
        public PersonContacts(int personId)
        {
            PersonId = personId;
        }

        public List<ContactInfo> ToList()
        {
            var list = new List<ContactInfo>();
            list.AddRange(PhoneNumbers);
            list.AddRange(SocialMedia);
            list.AddRange(Emails);
            list.AddRange(Others);
            return list;
        }
    }
}
