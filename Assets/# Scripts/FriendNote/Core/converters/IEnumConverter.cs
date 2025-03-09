using System;

namespace FriendNote.Core.Converters
{
    public interface IEnumConverter<T> where T : Enum
    {
        /// <summary>
        /// Получение названия, соответствующего значению T. Если его нет, то null
        /// </summary>
        string ToString(T element);
        /// <summary>
        /// Получение значения T, соответствующего названию
        /// </summary>
        T ToEnum(string elementName);
    }
}
