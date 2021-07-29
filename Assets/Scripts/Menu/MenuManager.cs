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
        private string SAVEPATH = "";
        private string GAMESCENE_NAME = "main";

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

        public MenuEvent CreateTabSwitchEvent(int menuIndex)
        {
            MenuEvent tabEvent = new SwitchTabEvent(menuIndex);
            return tabEvent;
        }

        public MenuEvent CreateTabSwitchEvent(string menuname)
        {
            MenuEvent tabEvent = new SwitchTabEvent(menuname);
            return tabEvent;
        }

        public MenuEvent CreateSceneSwitchEvent(string sceneName)
        {
            MenuEvent sceneEvent = new SwitchSceneEvent(sceneName);
            return sceneEvent;
        }

        public MenuEvent CreateQuitEvent()
        {
            MenuEvent quitEvent = new QuitEvent();
            return quitEvent;
        }

        public MenuEvent CreateEraseDataEvent(string path)
        {
            MenuEvent eraseEvent = new EraseEvent(path);
            return eraseEvent;
        }

        public MenuEvent CreateConfirmationEvent(string description)
        {
            MenuEvent confirmEvent = new ConfirmationEvent(description);
            return confirmEvent;
        }



        //specific button calls
        public void DoNewGameBySceneName(string sceneName)
        {
            DoNewGame(sceneName, SAVEPATH);
        }

        public void DoNewGameBySavePath(string savepath)
        {
            DoNewGame(GAMESCENE_NAME, savepath);
        }

        public void DoNewGame(string sceneName, string savepath)
        {
            DoConfirmation("Are you sure you wish to erase your data and start over?");
            DoConfirmation("Really?");
            queuedEvents.Add(CreateEraseDataEvent(savepath));
            queuedEvents.Add(CreateSceneSwitchEvent(sceneName));
        }

        public void DoLoadGame(string sceneName)
        {
            queuedEvents.Add(CreateSceneSwitchEvent(sceneName));
        }

        public void DoQuit()
        {
            DoConfirmation("Are you sure you wish to exit to desktop?");
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
                confirmationText.text = actionDescription;
            }
            confirmNextAction = true;
            LoadConfirmationTab();
        }

        private void CallQueuedEvents()
        {
            if (!confirmNextAction && queuedEvents.Count > 0)
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
            //throw new NotImplementedException();
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

