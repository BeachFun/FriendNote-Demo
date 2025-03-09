using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendNote.Core.DTO;
using FriendNote.Data.Repositories;
using FriendNote.Infrastructure;

namespace FriendNote.Data
{
    public class EntityDataService : IService, IDisposable
    {
        private ServiceStatus _status;
        private IRepositoryFactory _repoFactory;

        public ServiceStatus Status
        {
            get => _status;
            private set
            {
                Logger.Log($"EtityData service is {value.ToString()}");
                _status = value;
            }
        }


        #region [Методы] Запуск и инициализация сервиса

        /// <summary>
        /// Запуск сервиса управления даннымии
        /// </summary>
        public async void Startup()
        {
            Status = ServiceStatus.Initializing;

            while (Services.Config.Status != ServiceStatus.Started)
                await Task.Yield();

            _repoFactory = new RepoFactory();

            Status = ServiceStatus.Started;
        }

        public void Dispose()
        {
            if (Status != ServiceStatus.Started) return;

            Status = ServiceStatus.Shutdown;
        }

        #endregion


        /// <summary>
        /// Проверяет наличие базовых данных
        /// </summary>
        public bool? ExistEntity<T>(int id) where T : EntityBase, new()
        {
            return _repoFactory.GetRepo<T>()?.Exist(id);
        }

        /// <summary>
        /// Загрузка базовых данных
        /// </summary>
        /// <returns>Объект нужного типа или null - если передан неизвестный тип данных</returns>
        public T LoadEntity<T>(int id) where T : EntityBase, new()
        {
            return _repoFactory.GetRepo<T>()?.LoadById(id);
        }

        /// <summary>
        /// Сохранение базовых данных
        /// </summary>
        /// <returns>true/false в зависимости от успешности операции удаления. null - если передан неизвестный тип данных</returns>
        public bool SaveEntity<T>(T data) where T : EntityBase, new()
        {
            // Записи не сохраняются с ID = 0, даже первые
            return _repoFactory.GetRepo<T>()?.Save(data) > 0;
        }

        /// <summary>
        /// Удаление базовых данных
        /// </summary>
        /// <returns>true/false в зависимости от успешности операции удаления. null - если передан неизвестный тип данных</returns>
        public bool RemoveEntity<T>(T data) where T : EntityBase, new()
        {
            IRepository<T> repo = _repoFactory.GetRepo<T>();

            if (repo is null)
            {
                Logger.LogWarning($"Не найден репозиторий для удаления {typeof(T).Name}");
                return false;
            }

            return repo.Remove(data);
        }

        /// <summary>
        /// Получение списка людей без фильтра по умолчанию
        /// </summary>
        public List<PersonBasicInfo> LoadPersonList(string search = null)
        {
            try
            {
                var personRepo = (IPersonRepository)_repoFactory.GetRepo<PersonBasicInfo>();
                return personRepo.LoadAllPersons(search).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Получение дополнительной информации, связанной с человеком
        /// </summary>
        /// <returns>Категория информации в виде списка. В крайнем случае возвращается пустой список</returns>
        public List<T> LoadPersonRelatedInfo<T>(int personId) where T : EntityBase, IPersonRelatedInfo, new()
        {
            try
            {
                var relatedRepo = (IPersonRelatedInfoRepository<T>)_repoFactory.GetRepo<T>();
                return relatedRepo.LoadAllByPersonId(personId) as List<T>;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return new();
            }
        }
    }
}
