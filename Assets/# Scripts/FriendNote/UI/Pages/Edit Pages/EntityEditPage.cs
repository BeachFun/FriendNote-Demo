using FriendNote.Core.DTO;

namespace FriendNote.UI.Pages
{
    public abstract class EntityEditPage<T> : EditPage<T>
        where T : EntityBase, IPersonRelatedInfo, new()
    {
        public override void SaveData()
        {
            if (!CheckFillValidation()) return;

            Services.EntityData.SaveEntity(CollectData());
        }
    }
}
