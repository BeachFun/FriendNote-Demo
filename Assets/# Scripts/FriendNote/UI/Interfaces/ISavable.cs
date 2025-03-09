namespace FriendNote
{
    public interface ISavable<T>
    {
        void SaveData(T data);
    }
}
