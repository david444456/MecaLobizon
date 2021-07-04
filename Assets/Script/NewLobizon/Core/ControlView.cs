using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobizon.Core
{
    public class ControlView : MonoBehaviour
    {
        [SerializeField] GameObject levelButtons;
        [SerializeField] GameObject[] activitiesButtons;

        [Header("Buttons activities")]
        [SerializeField] Image[] imagesBackgroundButtonsLock;
        [SerializeField] Color colorBackGroundLocked;
        [SerializeField] Color colorBackGroundUnlocked;

        [Header("Mini games")]
        [SerializeField] GameObject BackGroundInfoMiniGame;
        [SerializeField] Text infoTextBackGround;
        [SerializeField] Image infoImageBackGround;
        [SerializeField] Button buttonExitMiniGameInfo;

        void Awake()
        {
            foreach (Image im in imagesBackgroundButtonsLock)
            {
                im.color = colorBackGroundLocked;
            }

            buttonExitMiniGameInfo.onClick.AddListener(DesactiveBackGroundInfoMiniGame);
        }

        public void ChangeColorBackGroundButtonsLock(int index) {
            imagesBackgroundButtonsLock[index].color = colorBackGroundUnlocked;
        }

        public void ActiveBackGroundInfoMiniGame(string infotext, Sprite spriteImageInfo) {
            BackGroundInfoMiniGame.SetActive(true);
            infoImageBackGround.sprite = spriteImageInfo;
            infoTextBackGround.text = infotext;
        }

        public void DesactiveBackGroundInfoMiniGame() {
            BackGroundInfoMiniGame.SetActive(false);
        }

        public void ActiveNewBackgroundButtons(int index) {
            levelButtons.SetActive(false);
            activitiesButtons[index].SetActive(true);
        }

        public void DesactiveNewBackGroundButtons(int index) {
            levelButtons.SetActive(true);
            activitiesButtons[index].SetActive(false);
        }
    }
}
