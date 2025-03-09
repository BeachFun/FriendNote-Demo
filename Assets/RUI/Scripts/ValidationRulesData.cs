using System.Globalization;

namespace RUI
{
    /// <summary>
    /// Правила для настройки валидации текста
    /// </summary>
    public class ValidationRulesData
    {
        /// <summary>
        /// Строка после ввода не должна быть пустой
        /// </summary>
        public bool IsNotNullOrEmpty { get; set; }

        /// <summary>
        /// Все символы являются допустимыми
        /// </summary>
        public bool IsAllCharactersValid { get; set; } = true;

        /// <summary>
        /// Допустимые категории символов
        /// </summary>
        public UnicodeCategory[] ValidCharactersCategory { get; set; }

        /// <summary>
        /// Отдельный список допустимых символов
        /// </summary>
        public string ValidCharacters { get; set; }
    }
}
