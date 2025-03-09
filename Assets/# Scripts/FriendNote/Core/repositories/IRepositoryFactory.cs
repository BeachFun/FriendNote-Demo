using FriendNote.Core.DTO;

namespace FriendNote.Data.Repositories
{
    public interface IRepositoryFactory
    {
        public IRepository<T> GetRepo<T>() where T : EntityBase, new();
        public IPersonRepository GetPersonRepo();
        public IPersonRelatedInfoRepository<T> GetRelatedRepo<T>() where T : EntityBase, IPersonRelatedInfo, new();
    }
}
