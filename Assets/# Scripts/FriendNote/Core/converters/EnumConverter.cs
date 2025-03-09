using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendNote.Core.Converters
{

    /// <summary>
    /// Базовый преобразователь данных для всех Enum-string converters.
    /// </summary>
    public abstract class EnumConverter<T> : IEnumConverter<T>
        where T : Enum
    {
        public Dictionary<T, string> Data { get; protected set; } = new();

        public string ToString(T element)
        {
            string str = null;
            Data.TryGetValue(element, out str);
            return str;
        }

        public T ToEnum(string elementName)
        {
            if (elementName is null || !Data.ContainsValue(elementName))
            {
                throw new ArgumentException($"Invalid {typeof(T).Name} type string.", nameof(elementName));
            }

            return Data.FirstOrDefault(x => x.Value == elementName).Key;
        }
    }
}
