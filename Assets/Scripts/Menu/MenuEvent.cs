using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement.Events
{
    [System.Serializable]
    public abstract class MenuEvent
    {
        [Header("Confirmation")]
        [SerializeField] protected string actionDescription;
        [SerializeField] protected bool requireConfirmation;

        public abstract void DoEvent();
        public bool RequiresConfirmation()
        {
            return requireConfirmation;
        }
        public void ForwardEvent()
        {
            
        }

        public MenuEvent(bool requireConfirmation, string actionDescription)
        {
            this.actionDescription = actionDescription;
            this.requireConfirmation = requireConfirmation;
        }

        public void SetConfirmation (bool value)
        {
            requireConfirmation = value;
        }

        public bool GetConfirmation ()
        {
            return requireConfirmation;
        }

        public void SetActionDescription(string actionDescription)
        {
            this.actionDescription = actionDescription;
        }

        public string GetActionDescription ()
        {
            return actionDescription;
        }
    }



    [System.Serializable]
    public class EraseEvent : MenuEvent
    {
        private string path;
        public override void DoEvent()
        {

        }

        public EraseEvent(bool requireConfirmation, string actionName, string path) : base(requireConfirmation, actionName)
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

        }

        public SwitchTabEvent(bool requireConfirmation, string actionName, int tabIndex) : base(requireConfirmation, actionName)
        {
            this.tabIndex = tabIndex;
        }
        public SwitchTabEvent(bool requireConfirmation, string actionName, string tabName) : base(requireConfirmation, actionName)
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

        }
        public SwitchSceneEvent(bool requireConfirmation, string actionName, string sceneName) : base(requireConfirmation, actionName)
        {
            this.sceneName = sceneName;
        }
    }

    [System.Serializable]
    public class QuitEvent : MenuEvent
    {
        public override void DoEvent()
        {

        }

        public QuitEvent(bool requireConfirmation, string actionName) : base(requireConfirmation, actionName)
        {

        }
    }

    [System.Serializable]
    public class ConfirmationEvent : MenuEvent
    {
        public override void DoEvent()
        {

        }

        public ConfirmationEvent(bool requireConfirmation, string actionName) : base(requireConfirmation, actionName)
        {

        }
    }
}