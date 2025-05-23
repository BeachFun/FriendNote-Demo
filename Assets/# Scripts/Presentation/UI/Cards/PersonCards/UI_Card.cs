namespace FriendNote.UI.Cards
{
    public abstract class UI_Card<T> : UI_DataPanel, IDataUpdatable<T>
        where T : class, new()
    {
        protected T _data;

        public virtual void UpdateData(T data)
        {
            _data = data;
        }
    }
}
