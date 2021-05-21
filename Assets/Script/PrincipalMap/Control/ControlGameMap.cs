using Board;
using General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] RegionalEvent regionalEventWinCondition;
        [SerializeField] GameObject GOButtonGoCABA = null;
        [SerializeField] GameObject GOButtonNewDiceMove = null;

        [Header("Menu condition")]
        [SerializeField] GameObject MenuGameObject = null;
        [SerializeField] GameObject MenuEventGameObject = null;
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

            if(PlayerData.playerData.Move == 0) ChangeHealth( initialHealth);
            else ChangeHealth(PlayerData.playerData.Move);

            if (PlayerData.playerData.Coin == 0) ChangeCoin( initialCoin);
            else ChangeCoin(PlayerData.playerData.Coin);
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            ActiveButtonDiceNewRollMove();
        }

        public void GoImmediatelyToCaba() {
            GOButtonNewDiceMove.SetActive(false);
            SetMovementPlayer(2);
        }

        public void EventMoveNewPosition() {
            SetMovementPlayer(GetNewRollMove());
        }

        public void EventCombat(ProgressionCombat progressionCombat, CharacterBoard characterBoard)
        {
            //save data and past parameters
            PlayerData.playerData.Move = actualHealth;
            PlayerData.playerData.Coin = actualCoin;
            PlayerData.playerData.progressionCombat = progressionCombat;
            PlayerData.playerData.characterBoardEnemy = characterBoard;

            //load scene
            PlayerData.playerData.LoadScene(2);
            print("Mortal Combat");
        }

        public void EventCoin(int coins)
        {
            ChangeCoin(coins);
            print("Tome coins, toma: " + coins);
        }

        public void EventSubtractHalfCoin() {
            actualCoin = actualCoin/2;
            textCoin.text = actualCoin.ToString();
        }

        public void EventHealth(int health)
        {
            ChangeHealth(health);
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
        }

        private void ActiveButtonDiceNewRollMove() {
            if (moveActive) return;
            viewGameMap.SetActiveButtonDiceNewRollMove(true);

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
            textCoin.text = actualCoin.ToString() + "/" + regionalEventWinCondition.winCondition.ToString();

            if (actualCoin >= regionalEventWinCondition.winCondition) {
                GOButtonGoCABA.SetActive(true);
            }
            //text
        }

        private void UpdateMenuFinishGameLoop(RegionalEvent regionalEvent)
        {
            MenuGameObject.SetActive(true);
            MenuEventGameObject.SetActive(false);
            playerArgMove.DiePlayer = true;
            textMenu.text = regionalEvent.GetPrincipalText();
            textMenuButton.text = regionalEvent.GetFirstButtonText();
            imageMenu.sprite = regionalEvent.GetSpriteBackGround();
        }
    }
}
