using System;

namespace FriendNote.Core
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}
