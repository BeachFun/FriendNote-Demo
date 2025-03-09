using SQLite;
using FriendNote.Core.Enums;

namespace FriendNote.Data.Tables
{
    [Table("Contact")]
    public sealed class ContactInfoORM : ITable, IPersonTableLink
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("contact_id")]
        public int? Id { get; set; } = -1;

        [NotNull, Column("person_id")]
        public int? PersonId { get; set; } = -1; // link of Person table

        [Column("contact_type")]
        public ContactTypeEnum ContactType { get; set; } // phone, email, social network

        [Column("contact_subtype")]
        public string ContactSubtype { get; set; }

        [Column("contact_data")]
        public string ContactData { get; set; }
    }
}
