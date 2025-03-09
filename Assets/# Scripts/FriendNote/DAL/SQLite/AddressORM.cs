using SQLite;

namespace FriendNote.Data.Tables
{
    [Table("Address")]
    public class AddressORM : ITable
    {
        [NotNull, PrimaryKey, AutoIncrement, Column("address_id")]
        public int? Id { get; set; } = -1;

        [Column("country")]
        public string Country { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("house")]
        public string House { get; set; }

        [Column("apartment")]
        public string Apartment { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }
    }
}
