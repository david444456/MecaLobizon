using Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincipalMap
{
    public class ControlGameMap : MonoBehaviour
    {
        [SerializeField] ControlDiceMovement controlDiceMovement;
        [SerializeField] PlayerArgMove playerArgMove;

        [Header("UI")]
        [SerializeField] Text textCoin;
        [SerializeField] Text textHealth;
        [SerializeField] Text textGoalCoinWin;
        [SerializeField] RegionalEvent regionalEventWinCondition;

        [Header("Menu condition")]
        [SerializeField] GameObject MenuGameObject = null;
        [SerializeField] Text textMenu;
        [SerializeField] Text textMenuButton;
        [SerializeField] Image imageMenu;
        [SerializeField] RegionalEvent regionalEventWinMenu;
        [SerializeField] RegionalEvent regionalEventLostMenu;

        [Header("Player var")]
        [SerializeField] int initialHealth = 50;
        [SerializeField] int initialCoin = 0;    

        ControlEvent controlEvent;
        ViewControlGameMap viewGameMap;

        int actualHealth = 0;
        public int actualCoin = 0;

        int lastMove = 1;
        bool moveActive = false;

        // Start is called before the first frame update
        void Awake()
        {
            viewGameMap = GetComponent<ViewControlGameMap>();
            controlEvent = GetComponent<ControlEvent>();

            ChangeHealth( initialHealth);
            ChangeCoin( initialCoin);

            textGoalCoinWin.text = regionalEventWinCondition.winCondition.ToString();
        }

        private void Start()
        {
            ActiveButtonDiceNewRollMove();
        }

        public void EventMoveNewPosition() {
            SetMovementPlayer(GetNewRollMove());
        }

        public void EventCombat()
        {
            print("Mortal Combat");
        }

        public void EventCoin(int coins)
        {
            ChangeCoin(coins);
            print("Tome coins, toma: " + coins);
        }

        public void EventHealth(int health)
        {
            ChangeHealth(health);
            print("Sos gracioso, toma: " + health);
        }

        public void EventFinishGame()
        {
            UpdateMenuFinishGameLoop(regionalEventWinMenu);
            print("Finalizaste el juego: ");
        }

        public void FinishEvent() {

            ActiveButtonDiceNewRollMove();
        }

        public int GetNewRollMove() {
            int rollMove = controlDiceMovement.GetNewDiceRoll();
            if (rollMove == lastMove) return GetNewRollMove();
            return rollMove;
        }

        public void SetMovementPlayer(int m_lastDiceMoveValue)
        {
            ChangeHealth(-1);
            moveActive = true;
            lastMove = m_lastDiceMoveValue;
            playerArgMove.NewMovementPlayer(m_lastDiceMoveValue);
        }

        public void FinishPath() {
            moveActive = false;
            controlEvent.ActiveNewEvent(lastMove);
            print("Termineeeee");
        }

        private void ActiveButtonDiceNewRollMove() {
            if (moveActive) return;
            viewGameMap.ActiveButtonDiceNewRollMove();

        }

        private void ChangeHealth(int newHealth) {
            actualHealth += newHealth;

            if (actualHealth <= 0) {
                UpdateMenuFinishGameLoop(regionalEventLostMenu);
                print("Perdiste cracksito");
            }

            textHealth.text = actualHealth.ToString();
            //text
        }

        private void ChangeCoin(int newCoin)
        {
            actualCoin += newCoin;
            textCoin.text = actualCoin.ToString() ;
            //text
        }

        private void UpdateMenuFinishGameLoop(RegionalEvent regionalEvent)
        {
            MenuGameObject.SetActive(true);
            textMenu.text = regionalEvent.GetPrincipalText();
            textMenuButton.text = regionalEvent.GetFirstButtonText();
            imageMenu.sprite = regionalEvent.GetSpriteBackGround();
        }
    }
}