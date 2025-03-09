using System.Collections.Generic;
using FriendNote.Core.DTO;

namespace FriendNote.Data.Repositories
{
    public interface IPersonRepository : IRepository<PersonBasicInfo>
    {
        /// <summary>
        /// Загрузка списка всех людей
        /// </summary>
        /// <returns></returns>
        IEnumerable<PersonBasicInfo> LoadAllPersons(string search = null);
    }
}
