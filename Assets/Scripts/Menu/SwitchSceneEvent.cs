using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    [System.Serializable]
    public class SwitchSceneEvent : MenuEventCarrier
    {
        [SerializeField] private string sceneName;
        public override void DoEvent()
        {

        }
    }
}
