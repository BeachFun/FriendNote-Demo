using UnityEngine;

namespace FriendNote.Infrastructure
{
    public static class Logger
    {
        /// <summary>
        /// Логирование информационных сообщений
        /// </summary>
        /// <param name="message">Сообщение выводящееся в консоль Unity</param>
        public static void Log(string message) => Debug.Log(message);

        /// <summary>
        /// Логирование предупреждений
        /// </summary>
        /// <param name="message">Сообщение выводящееся в консоль Unity</param>
        public static void LogWarning(string message) => Debug.LogWarning(message);

        /// <summary>
        /// Логирование ошибок
        /// </summary>
        /// <param name="message">Сообщение выводящееся в консоль Unity</param>
        public static void LogError(string message) => Debug.LogError(message);

        /// <summary>
        /// Логирование исключений с выводом стека вызовов
        /// </summary>
        /// <param name="exception">Ошибка выводящееся в консоль Unity</param>
        public static void LogException(System.Exception exception) => Debug.LogException(exception);

        /// <summary>
        /// Логирование с дополнительной информацией (например, с меткой или названием компонента)
        /// </summary>
        /// <param name="message">Сообщение выводящееся в консоль Unity</param>
        public static void LogWithTag(string tag, string message) => Debug.Log($"[{tag}] {message}");

        /// <summary>
        /// Логирование ошибок с дополнительной информацией
        /// </summary>
        /// <param name="message">Сообщение выводящееся в консоль Unity</param>
        public static void LogErrorWithTag(string tag, string message) => Debug.LogError($"[{tag}] {message}");
    }
}
