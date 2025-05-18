namespace FriendNote.UI
{
    public interface IDataUpdatable
    {
        void UpdateData(int personId);
    }

    public interface IDataUpdatable<T>
    {
        void UpdateData(T data);
    }
}
