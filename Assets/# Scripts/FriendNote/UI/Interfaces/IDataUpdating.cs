namespace FriendNote
{
    public interface IDataUpdating<T> where T : class
    {
        void DataUpdate(T data);
    }

    public interface IDataUpdating
    {
        void DataUpdate<T>(T data) where T : class;
    }
}
