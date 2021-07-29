using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement.Events
{
    [System.Serializable]
    public abstract class MenuEvent
    {

        public abstract void DoEvent();

        public MenuEvent()
        {

        }

    }



    [System.Serializable]
    public class EraseEvent : MenuEvent
    {
        private string path;
        public override void DoEvent()
        {
            MenuManager.current.EraseData(path);
        }

        public EraseEvent(string path) : base()
        {
            this.path = path;
        }

        public string GetPath()
        {
            return path;
        }
    }

    [System.Serializable]
    public class SwitchTabEvent : MenuEvent
    {
        [SerializeField] private string tabName = null;
        [SerializeField] private int tabIndex;
        public override void DoEvent()
        {
            if (tabName != null)
                MenuManager.current.SwitchTab(tabName);
            else
                MenuManager.current.SwitchTab(tabIndex);
        }

        public SwitchTabEvent(int tabIndex) : base()
        {
            this.tabIndex = tabIndex;
        }
        public SwitchTabEvent(string tabName) : base()
        {
            this.tabName = tabName;
        }
    }

    [System.Serializable]
    public class SwitchSceneEvent : MenuEvent
    {
        [SerializeField] private string sceneName;
        public override void DoEvent()
        {
            MenuManager.current.LoadScene(sceneName);
        }
        public SwitchSceneEvent(string sceneName) : base()
        {
            this.sceneName = sceneName;
        }
    }

    [System.Serializable]
    public class QuitEvent : MenuEvent
    {
        public override void DoEvent()
        {
            MenuManager.current.Quit();
        }

        public QuitEvent() : base()
        {

        }
    }

    [System.Serializable]
    public class ConfirmationEvent : MenuEvent
    {
        [SerializeField] string actionDescription;
        public override void DoEvent()
        {
            MenuManager.current.ActivateConfirmation(actionDescription);
        }

        public ConfirmationEvent(string actionDescription) : base()
        {
            this.actionDescription = actionDescription;
        }
    }
}