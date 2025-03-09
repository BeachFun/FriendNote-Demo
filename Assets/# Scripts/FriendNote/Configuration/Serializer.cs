using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FriendNote.Configuration
{
    /// <summary>
    /// Содержит средства сериализации и десериализации
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Метод для сериализации объекта в строку XML.
        /// </summary>
        public static string Serialize<T>(T obj, Encoding encoding = null)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Объект для сериализации не должен быть null.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var memoryStream = new MemoryStream();
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(memoryStream, obj);
                memoryStream.Position = 0L;

                using var reader = new StreamReader(memoryStream, encoding);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка при сериализации объекта.", ex);
            }
        }

        /// <summary>
        /// Метод для десериализации строки XML в объект.
        /// </summary>
        public static T Deserialize<T>(string xml, Encoding encoding = null)
        {
            if (xml == null)
                throw new ArgumentNullException(nameof(xml), "Строка XML для десериализации не должна быть null.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var memoryStream = new MemoryStream(encoding.GetBytes(xml));
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка при десериализации строки XML.", ex);
            }
        }

        /// <summary>
        /// Метод для ассинхронной сериализации объекта в файл XML.
        /// </summary>
        public static void SerializeToFile<T>(T obj, string filePath, Encoding encoding = null)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Объект для сериализации не должен быть null.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Create);
                var xmlSerializer = new XmlSerializer(typeof(T));

                xmlSerializer.Serialize(fileStream, obj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при сериализации в файл {filePath}.", ex);
            }
        }

        /// <summary>
        /// Метод для ассинхронной сериализации объекта в файл XML.
        /// </summary>
        public static async Task SerializeToFileAsync<T>(T obj, string filePath, Encoding encoding = null)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Объект для сериализации не должен быть null.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Create);
                var xmlSerializer = new XmlSerializer(typeof(T));

                await Task.Run(() => xmlSerializer.Serialize(fileStream, obj));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при сериализации в файл {filePath}.", ex);
            }
        }

        /// <summary>
        /// Метод для десериализации объекта из файла XML.
        /// </summary>
        public static T DeserializeFromFile<T>(string filePath, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "Путь к файлу не может быть null или пустым.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(T));

                return (T)xmlSerializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при десериализации файла {filePath}.", ex);
            }
        }

        /// <summary>
        /// Метод для ассинхронной десериализации объекта из файла XML.
        /// </summary>
        public static async Task<T> DeserializeFromFileAsync<T>(string filePath, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "Путь к файлу не может быть null или пустым.");

            encoding ??= Encoding.UTF8; // По умолчанию UTF-8

            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(T));

                return await Task.Run<T>(() => (T)xmlSerializer.Deserialize(fileStream));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при десериализации файла {filePath}.", ex);
            }
        }
    }
}
