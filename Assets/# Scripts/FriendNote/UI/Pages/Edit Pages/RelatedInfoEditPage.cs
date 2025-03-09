using FriendNote.Core.DTO;

namespace FriendNote.UI.Pages
{
    public abstract class RelatedInfoEditPage<T> : EntityEditPage<T>
        where T : EntityBase, IPersonRelatedInfo, new()
    {
        public int PersonId { get; set; }
    }
}
