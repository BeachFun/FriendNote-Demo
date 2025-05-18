using UnityEditorInternal;

namespace FriendNote.UI.Pages
{
    /// <summary>
    /// Информативная страница с автозагрузкой данных
    /// </summary>
    public abstract class PersonInfoPage : InfoPage<int>
    {
        // _pageData - это PersonId

        public PersonInfoPage()
        {
            _pageData.Value = -1;
        }
    }
}
