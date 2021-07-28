using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenuManagement.Events;
using UnityEngine.SceneManagement;
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
            UnloadConfirmationTab();
            LoadTab(0);
        }

        private void Update()
        {
            CallQueuedEvents();
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

        public MenuEvent CreateEraseDataEvent(string path, bool confirm = false)
        {
            MenuEvent eraseEvent = new EraseEvent(confirm, "erase your data", "");
            return eraseEvent;
        }

        public MenuEvent CreateConfirmationEvent(string description, bool confirm = false)
        {
            MenuEvent confirmEvent = new ConfirmationEvent(confirm, description);
            return confirmEvent;
        }



        //specific button calls
        public void DoNewGame(string sceneName, string path)
        {
            DoConfirmation("Erase your data and start over?");
            queuedEvents.Add(CreateEraseDataEvent(path, false));
            queuedEvents.Add(CreateSceneSwitchEvent(sceneName));
        }

        public void DoLoadGame()
        {
            
        }

        public void DoQuit()
        {
            DoConfirmation("suffer");
            queuedEvents.Add(CreateQuitEvent());
        }

        public void DoConfirmation(string actionDescription)
        {
            queuedEvents.Add(CreateConfirmationEvent(actionDescription));
        }

        public void ActivateConfirmation(string actionDescription)
        {
            if (confirmationText)
            {
                confirmationText.text = "Are you sure you wish to " + actionDescription;
            }
            confirmNextAction = true;
            LoadConfirmationTab();
        }


        private void CallQueuedEvents()
        {
            if(!confirmNextAction && queuedEvents.Count > 0)
            {
                queuedEvents[0].DoEvent();
                queuedEvents.RemoveAt(0);
            }
        }



        //eventfunctions
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
            if (index == selectedMenu)
            {
                selectedMenu = 0;
            }
            selectedMenu = index;
            menuTabs[index].EnableTabs();
        }

        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void EraseData(string path)
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            Application.Quit();
            print("Quit");
        }

        public void Confirm()
        {
            confirmNextAction = false;
            UnloadConfirmationTab();
        }

        public void Decline()
        {
            queuedEvents.Clear();
            confirmNextAction = false;
            UnloadConfirmationTab();
        }




        //internal tab management
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

    }
}

