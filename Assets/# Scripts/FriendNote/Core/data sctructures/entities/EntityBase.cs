namespace FriendNote.Core.DTO
{
    /// <summary>
    /// Базовый класс для всех идентифицируемых базовых структур данных приложения 
    /// </summary>
    public abstract class EntityBase : IIdentifiable
    {
        public int? Id { get; set; }
    }
}
