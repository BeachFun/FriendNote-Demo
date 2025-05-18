using System;
using System.Collections.Generic;
using System.Linq;
using FriendNote.Data.Mappers;
using FriendNote.Data.Tables;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using SQLite;

namespace FriendNote.Data.Repositories
{
    public class SQLitePersonRelatedInfoRepo<Tdto, Torm> : SQLiteRepoBase, IPersonRelatedInfoRepository<Tdto>, IRepository<Tdto>
        where Tdto : EntityBase, IIdentifiable, IPersonRelatedInfo
        where Torm : class, ITable, IPersonTableLink, new()
    {
        readonly IMapper<Torm, Tdto> _mapper;

        public SQLitePersonRelatedInfoRepo(SQLiteConnectionString connectionString) : base(connectionString)
        {
            _mapper = MapperFactory.GetMapper<Torm, Tdto>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.CreateTable<Torm>();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error when creating {typeof(Torm).Name} table in database\n{ex}");
                }
            }
        }

        public IEnumerable<Tdto> LoadAllByPersonId(int personId)
        {
            using (_db = new(_connectionString))
            {
                var list = _db.Table<Torm>()
                        .Where(x => x.PersonId == personId)
                        .ToList();
                try
                {
                    return list.Count == 0
                        ? Enumerable.Empty<Tdto>()
                        : list.Select(x => _mapper.ToDTO(x)).ToList();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error loading {typeof(Tdto).Name} list by PersonID {personId}\n{ex}");
                    return Enumerable.Empty<Tdto>();
                }
            }
        }

        public Tdto LoadById(int id)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    var skill = _db.Table<Torm>()
                                .Where(x => x.Id == id)
                                .FirstOrDefault();

                    if (skill == null)
                    {
                        Logger.LogWarning($"No {typeof(Tdto).Name} found for id {id}.");
                        throw new();
                    }

                    return _mapper.ToDTO(skill);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading {typeof(Tdto).Name} by ID {id}\n{ex}");
                return null;
            }
        }

        public bool Exist(int id)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    return _db.Table<Torm>().Where(p => p.Id == id).Count() > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error {typeof(Torm).Name} info with ID {id}\n{ex}");
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    var person = _db.Table<Torm>().FirstOrDefault(p => p.Id == id);

                    if (person is not null)
                    {
                        int count = _db.Delete<Torm>(id);
                        Logger.Log($"Delete {count} {typeof(Torm).Name} entries");
                        return count > 0;
                    }
                    else
                    {
                        Logger.LogWarning($"No {typeof(Tdto).Name} record found for id {id} to removing.");
                        throw new();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error removing {typeof(Tdto).Name} with ID {id}\n{ex}");
                return false;
            }
        }

        public bool Remove(Tdto data) => Remove(data.Id ?? DEFAULT_ID); // TODO: переделать

        public int Save(Tdto data)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    // Маппинг для получения ORM-объекта
                    Torm entry = _mapper.ToORM(data);

                    // Сохранение и получение кол-ва вставленных или измененных записей
                    int count = _db.InsertOrReplace(entry);

                    // Возвращение Id из записи, так как при сохранении в ORM-объекте Id заменяется на актуальной для сохраненной записи
                    return entry.Id.Value;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving {typeof(Tdto).Name} | Torm: {typeof(Torm).Name}\n{ex}");
                return EMPTY_ID;
            }
        }
    }

    public class SQLitePersonRelatedInfoRepo<Tdto, Torm1, Torm2> : SQLiteRepoBase, IPersonRelatedInfoRepository<Tdto>, IRepository<Tdto>
        where Tdto : EntityBase, IIdentifiable, IPersonRelatedInfo
        where Torm1 : class, ITable, IPersonTableLink, IAddressTableLink, new()
        where Torm2 : AddressORM, new()
    {
        readonly IMapper<Torm1, Torm2, Tdto> _mapper;

        public SQLitePersonRelatedInfoRepo(SQLiteConnectionString connectionString) : base(connectionString)
        {
            _mapper = MapperFactory.GetMapper<Torm1, Torm2, Tdto>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.CreateTable<Torm1>();
                    connection.CreateTable<Torm2>();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error when creating {typeof(Torm1).Name} table and {typeof(Torm2).Name} table in database\n{ex}");
                }
            }
        }

        public IEnumerable<Tdto> LoadAllByPersonId(int personId)
        {
            using (_db = new(_connectionString))
            {
                try
                {
                    var list = _db.Table<Torm1>()
                            .Where(x => x.PersonId == personId)
                            .ToList();

                    return list.Count == 0
                        ? Enumerable.Empty<Tdto>()
                        : list.Join(_db.Table<Torm2>(),
                                orm1 => orm1.AddressId,
                                address => address.Id,
                              (orm1, address) => new { orm1, address })
                            .Select(x => _mapper.ToDTO(x.orm1, x.address))
                            .ToList();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error loading {typeof(Tdto).Name} for personId {personId}\nTorm1: {typeof(Torm1).Name} | Torm2: {typeof(Torm2).Name}\n{ex}");
                    return Enumerable.Empty<Tdto>();
                }
            }
        }

        public Tdto LoadById(int id)
        {
            using (_db = new(_connectionString))
            {
                // Получаем данные из базы данных
                var orm1WithAddress = _db.Table<Torm1>()
                                          .Where(x => x.Id == id)
                                          .Join(_db.Table<Torm2>(),
                                                orm1 => orm1.AddressId,
                                                address => address.Id,
                                                (orm1, address) => new { orm1, address })
                                          .FirstOrDefault();

                // Проверяем не пустая ли запись
                if (orm1WithAddress == null)
                {
                    Logger.LogWarning($"No {typeof(Tdto).Name} record found for id {id}.");
                    return null;
                }

                // Преобразуем данные из таблицы в DTO
                try
                {
                    return _mapper.ToDTO(orm1WithAddress.orm1, orm1WithAddress.address);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error mapping {typeof(Tdto).Name} by Id {id}\n{ex}");
                    return null;
                }
            }
        }

        public bool Exist(int id)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    return _db.Table<Torm1>().Where(p => p.Id == id).Count() > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error {typeof(Torm1).Name} info with ID {id}\n{ex}");
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    _db.BeginTransaction();

                    // Поиск ORM записей по orm1 ID
                    var orm1entry = _db.Table<Torm1>().FirstOrDefault(x => x.Id == id);
                    var orm2entry = orm1entry is null ? null : _db.Table<Torm2>().FirstOrDefault(x => x.Id == orm1entry.AddressId);

                    // Удаление ORM2, если есть
                    if (orm2entry == null)
                    {
                        Logger.LogWarning($"{typeof(Torm2).Name} with ID {orm1entry.AddressId} not found.");
                    }
                    else
                    {
                        //  Удаление ORM2, связанного с ORM1
                        var count = _db.Delete(orm2entry);
                        if (count == 0)  // Проверка на неудачное удаление
                        {
                            Logger.LogError($"Failed to delete {typeof(Torm2).Name} with ID {orm2entry.Id}.");
                            _db.Rollback();
                            throw new();
                        }
                        Logger.Log($"Delete {count} {typeof(Torm2).Name} entries");
                    }

                    // Удаление ORM1, если есть
                    if (orm1entry == null)
                    {
                        Logger.LogWarning($"{typeof(Torm1).Name} with ID {id} not found.");
                        throw new();
                    }
                    else
                    {
                        // Удаление ORM1
                        var count = _db.Delete(orm1entry);
                        if (count == 0)  // Проверка на неудачное удаление
                        {
                            Logger.LogError($"Failed to delete {typeof(Torm1).Name} with ID {orm1entry.Id}.");
                            _db.Rollback();
                            throw new();
                        }
                        Logger.Log($"Delete {count} {typeof(Torm1).Name} entries");
                    }

                    _db.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error removing {typeof(Tdto).Name} by ID {id}\n{ex}");
                return false;
            }
        }

        public bool Remove(Tdto data) => Remove(data.Id ?? DEFAULT_ID); // TODO: переделать

        public int Save(Tdto entry)
        {
            try
            {
                using (_db = new(_connectionString))
                {
                    _db.BeginTransaction();

                    // Преобразование данных в ORM-формат
                    (Torm1, Torm2) pair = _mapper.ToORM(entry);

                    // Сохранение адреса ORM2
                    int count = _db.InsertOrReplace(pair.Item2);
                    if (count <= 0)  // Проверка на ошибку
                    {
                        Logger.LogError($"Failed to insert or replace address for {typeof(Tdto).Name} with ID {entry.Id}. Address ID is invalid.");
                        _db.Rollback();
                        throw new();
                    }

                    // Присвоение ID адреса записи ORM1
                    pair.Item1.AddressId = pair.Item2.Id.Value;

                    // Сохранение записи ORM1
                    count = _db.InsertOrReplace(pair.Item1);
                    if (count <= 0)  // Проверка на ошибку
                    {
                        Logger.LogError($"Failed to insert or replace {typeof(Tdto).Name} with ID {entry.Id}. {typeof(Tdto).Name} ID is invalid.");
                        _db.Rollback();
                        throw new();
                    }

                    _db.Commit();

                    // Возвращение Id из записи, так как при сохранении в ORM-объекте Id заменяется на актуальной для сохраненной записи
                    return pair.Item1.Id.Value;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving {typeof(Tdto).Name} with ID {entry.Id}\n{ex}");
                _db.Rollback();
                return EMPTY_ID;
            }
        }
    }
}
