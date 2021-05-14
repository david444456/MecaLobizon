using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class InternalSystemBoard : MonoBehaviour, ISystemBoard, IInternalSystemBoard
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

        void Awake()
        {
            controlCoinGame = GetComponent<ControlCoinInGame>();

            progCombat = PlayerData.playerData.progressionCombat;

            typeDiceMap = mediatorBoard.GetMapTypeRandom();
            mediatorBoard.SetTypeLootMap(typeDiceMap);
            mediatorBoard.CreatePrincipalWay(progCombat.largeOfTheListRow, progCombat.largeOfTheListColumn);


        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public void ExitBoardEvent()
        {
            //save data and past parameters
            PlayerData.playerData.SetAugmentCoin(  mediatorBoard.GetActualCoin());
            PlayerData.playerData.LoadScene(1);

            print("Exit the dungeon, (win) " + controlCoinGame.GetActualCoin());
        }

        public void LostTheGame()
        {
            print("LostTheGame");
        }

        public void CompletePathPlayer(int index) { //the index is +1
            TypeMap actualTypeMap = mediatorBoard.GetActualTypeMapByIndex(index);

            switch (actualTypeMap.GetTypeEventBoard())
            {
                case TypeEventBoard.Combat:
                    Vector2 newVector2 = mediatorBoard.GetPositioByIndex(index);
                    Vector2 vector2ChangeValue = new Vector2(newVector2.y, newVector2.x);
                    mediatorBoard.NewCombatTypeCharacter(vector2ChangeValue);
                    print("Combate!!! combate combate combate " + vector2ChangeValue);
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

        public void CompleteEventPlayer()
        {
            viewSystemDungeon.ActiveButtonDiceNewRollMove();
        }

        public void SetMovementPlayer(int newMoveValue)
        {
            mediatorBoard.NewMovementPlayer(newMoveValue);
        }

        private void newRollMovementActiveUI() {
            viewSystemDungeon.ActiveNewRollMovement();
        }

        IEnumerator StartGame() {
            yield return new WaitForSeconds(_timeToStartGame);
            newRollMovementActiveUI();
            _principalCamera.SetActive(true);
            _typeDicePlatform.SetActive(false);
        }
    }

    public interface IInternalSystemBoard {
        void CompletePathPlayer(int index);
        void CompleteEventPlayer();
    } 

}
