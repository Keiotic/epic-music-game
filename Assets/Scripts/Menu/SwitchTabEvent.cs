using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    [System.Serializable]
    public class SwitchTabEvent : MenuEventCarrier
    {
        [SerializeField] private string tabName;
        [SerializeField] private int tabIndex;
        public override void DoEvent()
        {

        }
    }
}
