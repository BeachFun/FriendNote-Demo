using System.Collections.Generic;
using FriendNote.Domain.DTO;

namespace FriendNote.Data.Repositories
{
    public interface IPersonRelatedInfoRepository<T> : IRepository<T>
        where T : EntityBase, IPersonRelatedInfo
    {
        /// <summary>
        /// Загрузка всей информации по personId, связанной с Person
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>Возвращает коллекцию данных или пустую коллекцию, если данных нет</returns>
        IEnumerable<T> LoadAllByPersonId(int personId);
    }
}
