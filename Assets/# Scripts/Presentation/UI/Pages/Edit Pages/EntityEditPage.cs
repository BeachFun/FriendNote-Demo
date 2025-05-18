using FriendNote.Data.Repositories;
using FriendNote.Domain.DTO;
using Zenject;

namespace FriendNote.UI.Pages
{
    public abstract class EntityEditPage<T> : EditPage<T>
        where T : EntityBase, IPersonRelatedInfo, new()
    {
        [Inject] private IRepositoryFactory _repoFactory;

        public int PersonId { get; set; }

        public override void SaveData()
        {
            if (!CheckFillValidation()) return;

            _repoFactory.GetRelatedRepo<T>().Save(CollectData());
            //Services.EntityData.SaveEntity(CollectData());
        }
    }
}
