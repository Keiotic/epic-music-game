using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenuManagement.Events;
namespace MenuManagement
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager current;
        [SerializeField] private List<MenuTab> menuTabs = new List<MenuTab>();
        [SerializeField] private MenuTab confirmationTab;
        [SerializeField] private Text confirmationText;
        private int selectedMenu = 0;
        private List<MenuEvent> queuedEvents = new List<MenuEvent>();
        private bool confirmNextAction;
        private Stack<int> visitedMenus;

        private void Start()
        {
            current = this;
            selectedMenu = 0;
        }

        private void Update()
        {

        }

        public void CallEvents()
        {

        }

        public void QueueEvent(MenuEvent ev)
        {
            queuedEvents.Add(ev);
        }


        public MenuEvent CreateTabSwitchEvent(int menuIndex, bool confirm = false)
        {
            MenuEvent tabEvent = new SwitchTabEvent(confirm, "switch menus", menuIndex);
            return tabEvent;
        }

        public MenuEvent CreateTabSwitchEvent(string menuname, bool confirm = false)
        {
            MenuEvent tabEvent = new SwitchTabEvent(confirm, "switch menus", menuname);
            return tabEvent;
        }

        public MenuEvent CreateSceneSwitchEvent(string sceneName, bool confirm = false)
        {
            MenuEvent sceneEvent = new SwitchSceneEvent(confirm, "proceed", sceneName);
            return sceneEvent;
        }

        public MenuEvent CreateQuitEvent(bool confirm = true)
        {
            MenuEvent quitEvent = new QuitEvent(confirm, "quit");
            return quitEvent;
        }

        public MenuEvent CreateEraseDataEvent(bool confirm = false)
        {
            MenuEvent eraseEvent = new EraseEvent(confirm, "erase your data", "");
            return eraseEvent;
        }

        public void DoNewGame(string sceneName)
        {
            queuedEvents.Add(CreateEraseDataEvent(false));
            queuedEvents.Add(CreateSceneSwitchEvent(sceneName));
            ForceConfirmation("erase your data and start over?");

        }

        public void ForceConfirmation(string actionDescription)
        {
            if (confirmationText)
            {
                confirmationText.text = "Are you sure you wish to " + actionDescription;
            }
            LoadConfirmationTab();
        }


        private void CallQueuedEvents()
        {
        }




        private void SwitchTab(int menuIndex)
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

        private void SwitchTab(string menuName)
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


        private void LoadTab(int index)
        {
            UnloadAllTabs();
            if (index == selectedMenu)
            {
                selectedMenu = 0;
            }
            selectedMenu = index;
            menuTabs[index].DisableTabs();

        }


        private void LoadConfirmationTab()
        {
            confirmationTab.EnableTabs();
        }


        private void UnloadConfirmationTab()
        {
            confirmationTab.DisableTabs();
        }


        private void UnloadAllTabs()
        {
            for (int i = 0; i < menuTabs.Count; i++)
            {
                MenuTab tab = menuTabs[i];
                tab.DisableTabs();
            }
        }



        private void Confirm()
        {
            confirmNextAction = false;
            UnloadConfirmationTab();
        }



        private void Decline()
        {
            queuedEvents.Clear();
            UnloadConfirmationTab();
        }

    }
}

