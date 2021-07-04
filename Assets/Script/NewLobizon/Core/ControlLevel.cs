using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Core
{
    public class ControlLevel : MonoBehaviour, IStartActivity
    {
        [SerializeField] ControlActivity[] controlActivities;

        int indexUnlocked = 0;
        ControlView view;

        void Start()
        {
            view = GetComponent<ControlView>();

            view.ChangeColorBackGroundButtonsLock(indexUnlocked);
        }

        public void StartNewActivity(int index) {
            if (index <= indexUnlocked)
            {
                view.ActiveNewBackgroundButtons(index);
            }
            else Debug.Log("Is lower than the unlock index");
        }

        public void CompleteActivity(int index) {
            if (index >= indexUnlocked)
            {
                indexUnlocked++;
                view.ChangeColorBackGroundButtonsLock(indexUnlocked);
            }
        }

        public void CompleteNewMiniActivity(int indexActivity) {
            controlActivities[indexActivity].CompleteNewActivity();
        }
    }

    
}
