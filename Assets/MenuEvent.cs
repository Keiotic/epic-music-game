using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    [System.Serializable]
    public abstract class MenuEvent
    {
        [SerializeField] private bool requireConfirmation;
        [Header("Confirmation")]
        [SerializeField] private string actionName;

        public abstract void DoEvent();
        public bool RequiresConfirmation()
        {
            return requireConfirmation;
        }
    }

    [System.Serializable]
    public class SwitchSceneEvent : MenuEvent
    {
        public override void DoEvent()
        {

        }
    }
}