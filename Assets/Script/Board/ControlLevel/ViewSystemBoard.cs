using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class ViewSystemBoard : View, ISystemBoard, IControlSlotMapEvent, IPlayerMove
    {
        [Header("Board")]
        [SerializeField] InternalSystemBoard internalSystemBoard;
        [SerializeField] GameObject panelConfirmExitDungeon;
        [SerializeField] GameObject panelExitButtonContinueDungeon;
        [SerializeField] Text textInfoRewards;

        [Header("UI button")]
        [SerializeField] Text textButtonChangeMove;
        [SerializeField] string firstTextInfoButton = "GO!";
        [SerializeField] string secondTextInfoButton = "Stop!";

        bool changeStateButtonInfo = false;

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

        public void PlayerDeadUIPanelReturnToMenu() {
            ExitBoardEvent();
            DesactiveButtonContinueInBoard();
        }

        public override void SetNewRandomDiceMove()
        {
            m_lastDiceMoveValue = MediatorBoard.Mediator.GetNewDiceRoll();
            StartCoroutine(RollDiceMove());
            
        }

        public void ChangeMoveStatePlayer(bool newState)
        {
            changeStateButtonInfo = !changeStateButtonInfo;
            textButtonChangeMove.text = changeStateButtonInfo ? firstTextInfoButton : secondTextInfoButton;

            internalSystemBoard.ChangeMoveStatePlayer(changeStateButtonInfo);
        }

        private void DesactiveButtonContinueInBoard() => panelExitButtonContinueDungeon.SetActive(false);
    }
}
