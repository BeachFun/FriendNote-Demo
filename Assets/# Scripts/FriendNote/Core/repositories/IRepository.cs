using FriendNote.Core.DTO;

namespace FriendNote.Data.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Проверка наличия записи
        /// </summary>
        bool Exist(int id);

        /// <summary>
        /// Сохранение информации
        /// </summary>
        /// <returns>Id сохраненной записи или -1, если операция не удалась</returns>
        int Save(T entry);

        /// <summary>
        /// Удаление информации по Id
        /// </summary>
        /// <returns>true/false в зависимости от успешности операции удаления</returns>
        bool Remove(int id);

        /// <summary>
        /// Удаление информации
        /// </summary>
        /// <returns>true/false в зависимости от успешности операции удаления</returns>
        bool Remove(T entry);

        /// <summary>
        /// Загрузка информации по Id
        /// </summary>
        T LoadById(int id);
    }
}
