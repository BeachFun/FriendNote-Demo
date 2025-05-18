using System.Collections.Generic;
using FriendNote.Core;
using FriendNote.Domain.DTO;

namespace FriendNote.Domain
{
    public interface IEntityDataService : IService
    {
        bool? ExistEntity<T>(int id) where T : EntityBase, new();
        T LoadEntity<T>(int id) where T : EntityBase, new();
        List<Person> LoadPersonList(string search = null);
        List<T> LoadPersonRelatedInfo<T>(int personId) where T : EntityBase, IPersonRelatedInfo, new();
        bool RemoveEntity<T>(T data) where T : EntityBase, new();
        bool SaveEntity<T>(T data) where T : EntityBase, new();
    }
}