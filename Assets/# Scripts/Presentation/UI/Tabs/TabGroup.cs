using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FriendNote.UI
{
    /// <summary>
    /// ��������� �������������� ����� ��������� ��������� ����
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        // ! ����� ����� ��������� �������, ������� ������ ������������ ������� ������ ��������������� ������� �������

        [SerializeField] private List<TabButton> tabButtons; // ������ ������������
        [SerializeField] private List<GameObject> sectionToSwap; // ������� ��� ������������
        [Space, SerializeField] private uint _currentSelectedButton; // ������� ��������� ������

        [Header("Button settings")]
        [SerializeField] private Color tabIdle;
        [SerializeField] private Color tabActive;

        [Header("Buffer")]
        [SerializeField] private TabButton prevSelectedTab;
        [SerializeField] private TabButton selectedTab;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_currentSelectedButton - 1 < tabButtons.Count && tabButtons.All(x => x != null))
            {
                OnTabSelected(tabButtons[(int)_currentSelectedButton - 1]);
            }
        }
#endif

        private void Start()
        {
            if (selectedTab is null) return;

            if (selectedTab.Background != null)
            {
                selectedTab.Background.color = tabActive;
            }
            if (selectedTab.Caption != null)
            {
                selectedTab.Caption.color = tabActive;
            }

            OnTabSelected(selectedTab);
        }

        /// <summary>
        /// �������� � ������ ������ � ������ ������ ������������, ���� ��� ��� �� �������
        /// </summary>
        /// <param name="button">������</param>
        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            if (button.Background != null)
            {
                button.Background.color = tabIdle;
            }
            if (button.Caption != null)
            {
                button.Caption.color = tabIdle;
            }

            if (!tabButtons.Contains(button))
            {
                tabButtons.Add(button);
            }
        }

        /// <summary>
        /// ������������ ������� �� ������
        /// </summary>
        /// <param name="button"></param>
        public void OnTabSelected(TabButton button)
        {
            ResetTabs();
            selectedTab = button;

            int index = button.transform.GetSiblingIndex();
            SwitchTab(index);
        }


        /// <summary>
        /// ����������� �� ��������� ������� � �������
        /// </summary>
        public void SwitchTab(int index)
        {
            prevSelectedTab = selectedTab;

            try
            {
                if (selectedTab.Background != null)
                {
                    selectedTab.Background.color = tabActive;
                }
                if (selectedTab.Caption != null)
                {
                    selectedTab.Caption.color = tabActive;
                }

                for (int i = 0; i < tabButtons.Count; i++)
                {
                    if (i == index)
                    {
                        sectionToSwap[i].SetActive(true);
                    }
                    else
                    {
                        sectionToSwap[i].SetActive(false);
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// ������� ���� ������ � ������� ���������
        /// </summary>
        private void ResetTabs()
        {
            if (prevSelectedTab.Background != null)
            {
                prevSelectedTab.Background.color = tabIdle;
            }
            if (prevSelectedTab.Caption != null)
            {
                prevSelectedTab.Caption.color = tabIdle;
            }

            foreach (TabButton button in tabButtons)
            {
                if (selectedTab != null && button == selectedTab)
                {
                    continue;
                }

                if (button.Background != null)
                {
                    button.Background.color = tabIdle;
                }
                if (button.Caption != null)
                {
                    button.Caption.color = tabIdle;
                }

                button?.OnTabUnactive?.Invoke();
            }
        }
    }
}
