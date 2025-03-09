using FriendNote.Core.DTO;
using Observer;

namespace FriendNote.UI.Pages
{
    public class InfoPage_PersonEducation : EntityRelatedInfoPage<Education>
    {
        protected override void Awake()
        {
            base.Awake();
            Messenger.AddListener(Notices.UINotice.EDIT_PAGE_PERSON_EDUCATION_CLOSED, OnInfoUpdate);
        }

        protected override void OnDestroy()
        {
            Messenger.RemoveListener(Notices.UINotice.EDIT_PAGE_PERSON_EDUCATION_CLOSED, OnInfoUpdate);
        }
    }
}
