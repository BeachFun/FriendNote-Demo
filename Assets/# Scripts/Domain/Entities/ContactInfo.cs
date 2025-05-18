using FriendNote.Core.Enums;

namespace FriendNote.Domain.DTO
{
    public class ContactInfo : EntityBase, IPersonRelatedInfo
    {
        public int? PersonId { get; set; } = -1; // Ссылка на Person
        public ContactTypeEnum ContactType { get; set; } // телефон, email, соц. сеть и т.д.
        public string ContactSubtype { get; set; } // Дополнительная информация о типе контакта (например, домашний, рабочий телефон)
        public string ContactData { get; set; } // Данные контакта (например, номер телефона, email, ссылку на соц. сеть)
    }
}
