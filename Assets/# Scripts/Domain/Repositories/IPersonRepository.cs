using System.Collections.Generic;
using FriendNote.Domain.DTO;

namespace FriendNote.Data.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Загрузка списка всех людей
        /// </summary>
        /// <returns></returns>
        IEnumerable<Person> LoadAllPersons(string search = null);
    }
}
