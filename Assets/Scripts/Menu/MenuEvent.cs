using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    [System.Serializable]
    public abstract class MenuEventCarrier : MonoBehaviour
    {
        [SerializeField] protected bool requireConfirmation;
        [Header("Confirmation")]
        [SerializeField] protected string actionName;

        public abstract void DoEvent();
        public bool RequiresConfirmation()
        {
            return requireConfirmation;
        }
        public void ForwardEvent()
        {
            MenuManager.current.CallEvent(this);
        }
    }
}