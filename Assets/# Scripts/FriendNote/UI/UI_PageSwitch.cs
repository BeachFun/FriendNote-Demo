using System.Linq;
using FriendNote.UI.Pages;
using UnityEngine;

namespace FriendNote.UI
{
    [ExecuteAlways]
    public class UI_PageSwitch : MonoBehaviour
    {
        [SerializeField] private Page[] _pages;
        [Space]
        [SerializeField] private uint _currentActivePage;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (_currentActivePage < _pages.Length && _pages.All(x => x != null))
            {
                foreach (var page in _pages) page.Close();
                _pages[_currentActivePage].Open();
            }
        }
#endif
    }
}
