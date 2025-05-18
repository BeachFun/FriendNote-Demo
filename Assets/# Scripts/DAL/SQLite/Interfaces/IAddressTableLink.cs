namespace FriendNote.Data.Tables
{
    public interface IAddressTableLink : IForeignKey<AddressORM>
    {
        int? AddressId { get; set; }
    }
}