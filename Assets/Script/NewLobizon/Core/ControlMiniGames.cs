using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobizon.Core
{
    public class ControlMiniGames : MonoBehaviour
    {

        [SerializeField] MiniGame[] miniGames;

        [Header("Mini games UI")]
        [SerializeField] Button buttonConfirmMiniGame;

        ControlView view;

        int _indexActivity = 0;
        int _lastIndex = 0;


        void Start()
        {
            view = GetComponent<ControlView>();

            buttonConfirmMiniGame.onClick.AddListener(StartNewMiniGame);
        }

        public void PrepareNewMiniGame(int index, int indexActualActivity) {
            view.ActiveBackGroundInfoMiniGame("Miaukai", miniGames[index].spriteThisMiniGame);

            _lastIndex = index;
            _indexActivity = indexActualActivity;
        }

        public void StartNewMiniGame() {
            miniGames[_lastIndex].StartNewMiniGame();
            view.DesactiveBackGroundInfoMiniGame();
        }

        public void WinMiniGame() {
            GetComponent<ControlLevel>().CompleteNewMiniActivity(_indexActivity);
            view.DesactiveBackGroundInfoMiniGame();
        }
    }
}