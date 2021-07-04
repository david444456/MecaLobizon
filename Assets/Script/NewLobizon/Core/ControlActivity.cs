using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobizon.Core
{
    public class ControlActivity : MonoBehaviour, IStartActivity
    {

        public int indexActualControlActivity = 0;
        [SerializeField] int[] indexMiniGamesButtons;
        [SerializeField] int totalActivitiesInside = 3;

        [Header("UI")]
        [SerializeField] Color colorBackGroundLocked;
        [SerializeField] Color colorBackGroundUnlocked;
        [SerializeField] Image[] imagesBackgroundButtons;

        ControlMiniGames controlMini;
        ControlLevel controlLevel;

        bool[] completesActivities;
        int _lastIndexActivity = 0;

        void Start()
        {
            foreach (Image im in imagesBackgroundButtons) {
                im.color = colorBackGroundLocked;
            }

            controlLevel = FindObjectOfType<ControlLevel>();
            controlMini = FindObjectOfType<ControlMiniGames>();
            completesActivities = new bool[totalActivitiesInside];
        }

        public void StartNewActivity(int indexButton) {
            controlMini.PrepareNewMiniGame(indexMiniGamesButtons[indexButton], indexActualControlActivity);
            _lastIndexActivity = indexButton;
        }

        public void CompleteNewActivity() {
            completesActivities[_lastIndexActivity] = true;
            imagesBackgroundButtons[_lastIndexActivity].color = colorBackGroundUnlocked;

            if (verificatedAllActivitiesComplete()) {
                print(true);
                controlLevel.CompleteActivity(indexActualControlActivity);
            }

        }

        private bool verificatedAllActivitiesComplete() {
            bool res = true;

            foreach (bool r in completesActivities) {
                res = r && res;
            }

            return res;
        }

    }
}
