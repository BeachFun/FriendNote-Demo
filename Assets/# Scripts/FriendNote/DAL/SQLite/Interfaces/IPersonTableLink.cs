namespace FriendNote.Data.Tables
{
    public interface IPersonTableLink : IForeignKey<PersonORM>
    {
        int? PersonId { get; set; }
    }
}
