using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace MenuManagement
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager current;
        [SerializeField] private List<MenuTab> menuTabs = new List<MenuTab>();
        [SerializeField] private MenuTab confirmationTab;
        private int selectedMenu = -1;
        private MenuEventCarrier queuedEvent;

        private void Start()
        {
            current = this;
            selectedMenu = -1;
        }

        private void Update()
        {

        }

        public void CallEvent(MenuEventCarrier ev)
        {
            queuedEvent = ev;
            if(ev.RequiresConfirmation())
            {
                LoadConfirmationTab();
            }
            else
            {
                ev.DoEvent();
            }
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
                MenuTab tab = menuTabs[i];
                if (menuName == tab.name)
                {
                    LoadTab(i);
                    return;
                }
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

        public void LoadConfirmationTab()
        {
            confirmationTab.EnableTabs();
        }

        public void UnloadConfirmationTab()
        {
            confirmationTab.DisableTabs();
        }

        public void UnloadAllTabs()
        {
            for(int i = 0; i < menuTabs.Count; i++)
            {
                MenuTab tab = menuTabs[i];
                tab.DisableTabs();
            }
        }

        public void Confirm ()
        {
            queuedEvent.DoEvent();
            UnloadConfirmationTab();
        }

        public void Decline ()
        {
            UnloadConfirmationTab();
        }

    }
}

