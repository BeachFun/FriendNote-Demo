using System;
using System.Collections.Generic;
using System.Linq;
using FriendNote.Core.DTO;
using FriendNote.Data.Mappers;
using FriendNote.Data.Tables;
using FriendNote.Infrastructure;
using SQLite;

namespace FriendNote.Data.Repositories
{
    public class SQLitePersonRepo : SQLiteRepoBase, IPersonRepository, IRepository<PersonBasicInfo>
    {
        private readonly PersonBasicInfoMapper _mapper;


        public SQLitePersonRepo(SQLiteConnectionString connectionString) : base(connectionString)
        {
            _mapper = new();

            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.CreateTable<PersonORM>();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error when creating {typeof(PersonORM).Name} table in database\n{ex}");
                }
            }
        }


        public IEnumerable<PersonBasicInfo> LoadAllPersons(string search = null)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    if (string.IsNullOrEmpty(search))
                    {
                        return _db.Table<PersonORM>()
                            .Select(x => _mapper.ToDTO(x))
                            .ToList();
                    }
                    else
                    {
                        return _db.Table<PersonORM>()
                            .Select(x => _mapper.ToDTO(x))
                            .Where(x => x.Nickname.Contains(search) || x.Name.Contains(search) || x.Surname.Contains(search))
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading person list\n{ex}");
                return Enumerable.Empty<PersonBasicInfo>();
            }
        }

        public PersonBasicInfo LoadById(int personId)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    var personEntry = _db.Table<PersonORM>().Where(x => x.Id == personId).FirstOrDefault();

                    if (personEntry == null)
                    {
                        Logger.LogWarning($"No person found for id {personId}.");
                    }

                    return _mapper.ToDTO(personEntry);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error laoding person basic info with ID {personId}\n{ex}");
                return null;
            }
        }

        public bool Exist(int personId)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    return _db.Table<PersonORM>().Where(p => p.Id == personId).Count() > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error checking person basic info with ID {personId}\n{ex}");
                return false;
            }
        }

        public int Save(PersonBasicInfo data)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    // Маппинг для получения ORM-объекта
                    PersonORM entry = _mapper.ToORM(data);

                    // Получение кол-ва вставленных или измененных записей
                    _db.InsertOrReplace(entry);

                    // Возвращение Id из записи, так как при сохранении в ORM-объекте Id заменяется на актуальной для сохраненной записи
                    return entry.Id.Value;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving person basic info\n{ex}");
                return EMPTY_ID;
            }
        }

        public bool Remove(int personId)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    var person = _db.Table<PersonORM>().FirstOrDefault(p => p.Id == personId);

                    if (person is not null)
                    {
                        int count = _db.Delete<PersonORM>(personId);
                        return count > 0;
                    }
                    else
                    {
                        Logger.LogWarning($"No person record found for id {personId} to removing.");
                        throw new();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error removing person basic info with ID {personId}\n{ex}");
                return false;
            }
        }

        public bool Remove(PersonBasicInfo data) => this.Remove(data.Id ?? DEFAULT_ID); // TODO: переделать
    }
}
