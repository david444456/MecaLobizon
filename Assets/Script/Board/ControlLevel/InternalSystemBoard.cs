using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class InternalSystemBoard : MonoBehaviour, ISystemBoard, IInternalSystemBoard, IPlayerMove, IAnimationDeadPlayer
    {
        [SerializeField] MediatorBoard mediatorBoard;
        [SerializeField] ViewSystemBoard viewSystemDungeon;

        [Header("Time start")]
        [SerializeField] GameObject _principalCamera = null;
        [SerializeField] GameObject _typeDicePlatform = null;
        [SerializeField] float _timeToStartGame = 2.7f;

        ControlCoinInGame controlCoinGame;
        TypeLootMap typeDiceMap;
        ProgressionCombat progCombat;

        bool IsDeadPlayer = false;

        void Awake()
        {
            controlCoinGame = GetComponent<ControlCoinInGame>();

            if (PlayerData.playerData == null) print("Null");
            progCombat = PlayerData.playerData.progressionCombat;

            typeDiceMap = mediatorBoard.GetMapTypeRandom();
            mediatorBoard.SetTypeLootMap(typeDiceMap);

            mediatorBoard.CreatePrincipalWay(progCombat.largeOfTheListRow, progCombat.largeOfTheListColumn);


        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public ProgressionCombat GetProgressionCombat() => progCombat;

        public void ExitBoardEvent()
        {
            int coinReward = 0;
            //save data and past parameters
            if (!IsDeadPlayer)
                coinReward = mediatorBoard.GetActualCoin();
            else
                coinReward = mediatorBoard.GetActualCoin()/2;

            PlayerData.playerData.SetAugmentCoin(coinReward);
            PlayerData.playerData.LoadScene(1);

            print("Exit the dungeon, (win) " + controlCoinGame.GetActualCoin());
        }

        public void LostTheGame()
        {
            IsDeadPlayer = true;
            ChangeMoveStatePlayer(true);
            print("LostTheGame");
        }

        public void EventAnimationDiePlayer()
        {
            viewSystemDungeon.PlayerDeadUIPanelReturnToMenu();
        }

        public void CompletePathPlayer(int index) { //the index is +1
            TypeMap actualTypeMap = mediatorBoard.GetActualTypeMapByIndex(index);

            switch (actualTypeMap.GetTypeEventBoard())
            {
                case TypeEventBoard.Combat:
                    CharacterBoard characterBoardEnemy = mediatorBoard.GetSlotEnemy(index-1);

                    if (characterBoardEnemy != null) {

                        Vector2 newVector2 = mediatorBoard.GetPositioByIndex(index-1);
                        Vector2 vector2ChangeValue = new Vector2(newVector2.y, newVector2.x);
                        mediatorBoard.NewCombatTypeCharacter(vector2ChangeValue, mediatorBoard.GetActualInstanceEnemy(index-1), characterBoardEnemy);
                        ChangeMoveStatePlayer(true);
                        SetActiveButtonDiceNewRollMove(false);
                        print("enemy in this position: " + vector2ChangeValue);
                    }

                    print("Combate!!! combate combate combate ");
                    break;
                case TypeEventBoard.Event:
                    mediatorBoard.StartNewEventMap(actualTypeMap);
                    viewSystemDungeon.StartNewEventMap(actualTypeMap);
                    print("Recompensame esta");
                    break;
                case TypeEventBoard.Exit:
                    print("Exit");
                    break;
            }
        }

        public void ChangeMoveStatePlayer(bool newState) => mediatorBoard.ChangeMoveStatePlayer(newState);

        public void CompleteEventPlayer()
        {
            if (!IsDeadPlayer)
            {
                if(!mediatorBoard.GetActualStateCombat()) ChangeMoveStatePlayer(false);
                SetActiveButtonDiceNewRollMove(true);
            }
        }

        private void newRollMovementActiveUI() {
            SetActiveButtonDiceNewRollMove(true);
        }

        IEnumerator StartGame() {
            yield return new WaitForSeconds(_timeToStartGame);
            newRollMovementActiveUI();
            _principalCamera.SetActive(true);
            _typeDicePlatform.SetActive(false);
        }

        private void SetActiveButtonDiceNewRollMove(bool newState) {
            viewSystemDungeon.SetActiveButtonDiceNewRollMove(newState);
        }


    }

    public interface IInternalSystemBoard {
        void CompletePathPlayer(int index);
        void CompleteEventPlayer();
    } 

}
