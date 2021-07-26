using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuManagement
{
    [System.Serializable]
    public class MenuTab
    {
        [SerializeField] internal string name;
        [SerializeField] internal List<GameObject> tabElements = new List<GameObject>();

        public void EnableTabs()
        {
            for (int i = 0; i < tabElements.Count; i++)
            {
                tabElements[i].SetActive(true);
            }
        }

        public void DisableTabs()
        {
            for (int i = 0; i < tabElements.Count; i++)
            {
                tabElements[i].SetActive(false);
            }
        }
    }
}