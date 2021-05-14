using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class ViewSystemBoard : View, ISystemBoard, IControlSlotMapEvent
    {
        [Header("Board")]
        [SerializeField] InternalSystemBoard internalSystemBoard;
        [SerializeField] GameObject panelConfirmExitDungeon;
        [SerializeField] Text textInfoRewards;

        public void ExitBoardEvent()
        {
            textInfoRewards.text = MediatorBoard.Mediator.GetActualCoin().ToString();
            panelConfirmExitDungeon.SetActive(true);
        }

        public void ConfirmExitTheDungeon() {
            internalSystemBoard.ExitBoardEvent();
        }

        public void LostTheGame()
        {
            internalSystemBoard.LostTheGame();
        }

        public override void SetNewMovementPlayer() //button
        {
            internalSystemBoard.SetMovementPlayer(m_lastDiceMoveValue);
        }

        public override void SetNewRandomDiceMove()
        {
            m_lastDiceMoveValue = MediatorBoard.Mediator.GetNewDiceRoll();
            StartCoroutine(RollDiceMove());
        }
    }
}
