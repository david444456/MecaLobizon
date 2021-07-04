using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Core {
    public class RunnerMiniGame : MiniGame
    {
        ControlMiniGames miniGames;

        private void Start()
        {
            miniGames = FindObjectOfType<ControlMiniGames>();
        }

        public override void StartNewMiniGame()
        {
            Debug.Log("Iniciar mini game runnner");
            StartCoroutine(TerminatedActivityAfterTime());
        }

        IEnumerator TerminatedActivityAfterTime() {
            yield return new WaitForSeconds(1);
            miniGames.WinMiniGame();

        }
    }
}
