using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MenuManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<MenuTab> menuTabs = new List<MenuTab>();
        private int selectedMenu = -1;
        private MenuEvent queuedEvent;

        private void Start()
        {
            selectedMenu = -1;
        }

        public void CallEvent(MenuEvent ev)
        {
            queuedEvent = ev;
        }

        public void SwitchTab(int menuIndex)
        {
            if (menuIndex < menuTabs.Count)
            {
                LoadTab(menuIndex);
                return;
            }
            else
            {
                throw new System.IndexOutOfRangeException();
            }
        }
        public void SwitchTab(string menuName)
        {
            for (int i = 0; i < menuTabs.Count; i++)
            {
                LoadTab(i);
                return;
            }
        }

        public void LoadTab(int index)
        {
            UnloadAllTabs();
            if(index == selectedMenu)
            {
                selectedMenu = -1;
            }
            else
            {
                selectedMenu = index;
                menuTabs[index].DisableTabs();
            }
        }

        public void UnloadAllTabs()
        {
            for(int i = 0; i < menuTabs.Count; i++)
            {
                MenuTab tab = menuTabs[i];
            }
        }

    }
}

