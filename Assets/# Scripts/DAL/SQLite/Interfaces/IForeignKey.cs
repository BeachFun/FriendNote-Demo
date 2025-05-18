namespace FriendNote.Data.Tables
{
    public interface IForeignKey<T>
    {
        public string LinkTableName
        {
            get => nameof(T);
        }
    }
}
