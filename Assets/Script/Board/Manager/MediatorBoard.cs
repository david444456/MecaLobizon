using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class MediatorBoard : MonoBehaviour, IViewWayBoard, IWayBoard, IDiceMovement, IPlayerMove, ICombatSystem,
        IPlayerHealth, IControlSlotMapEvent, IControlCoinInGame, ISystemBoard, IControlTypeDice, IInternalSystemBoard
    {
        public static MediatorBoard Mediator;

        [Header("Class")]
        [SerializeField] ViewWayBoard viewWayBoard;
        [SerializeField] PlayerDunMove playerDunMove;
        [SerializeField] ControlDiceMovement ctrolDiceMove;
        [SerializeField] PlayerDunMove playerMoveBoard;
        [SerializeField] CombatSystem combatSystem;
        [SerializeField] PlayerDunHealth playerDunHealth;
        [SerializeField] ControlSlotEvent slotEvent;
        [SerializeField] ControlCoinInGame coinInGame;
        [SerializeField] ViewSystemBoard viewSystemBoard;
        [SerializeField] ControlDiceType controlDiceType;
        [SerializeField] InternalSystemBoard internalSystemBoard;

        private void Awake()
        {
            if (Mediator == null)
            {
                Mediator = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public TypeMap GetActualTypeMapByIndex(int index) => viewWayBoard.GetActualTypeMapByIndex(index);

        public void SetTypeLootMap(TypeLootMap newType) => viewWayBoard.SetTypeLootMap(newType);

        public void CreatePrincipalWay(int row, int column) => viewWayBoard.CreatePrincipalWay(row, column);

        public List<Vector2> GetTheRealWayMovePlayer() => viewWayBoard.GetTheRealWayMovePlayer();

        public int GetNewDiceRoll() => ctrolDiceMove.GetNewDiceRoll();

        public void NewMovementPlayer(int value) => playerMoveBoard.NewMovementPlayer(value);

        public void NewCombatTypeCharacter(Vector2 lastPosition) => combatSystem.NewCombatTypeCharacter(lastPosition);

        public int GetHealth() => playerDunHealth.GetHealth();

        public void SetNewHealth(int value) => playerDunHealth.SetNewHealth(value);

        public void StartNewEventMap(TypeMap typeMap) => slotEvent.StartNewEventMap(typeMap);

        public int GetActualCoin() => coinInGame.GetActualCoin();

        public void SetCoinRewardEvent(int count) => coinInGame.SetCoinRewardEvent(count);

        public void ExitBoardEvent() => viewSystemBoard.ExitBoardEvent();

        public void LostTheGame() => viewSystemBoard.LostTheGame();

        public TypeLootMap GetMapTypeRandom() => controlDiceType.GetMapTypeRandom();

        public void CompletePathPlayer(int index) => internalSystemBoard.CompletePathPlayer(index);

        public void CompleteEventPlayer() => internalSystemBoard.CompleteEventPlayer();

        public Vector2 GetPositioByIndex(int index) => playerDunMove.GetPositioByIndex(index);

        public void ChangePositionPlayerToCombat(Vector3 vector3Position) => playerDunHealth.ChangePositionPlayerToCombat(vector3Position);

        public List<AbilitiesCharacter> GetAbilitiesPlayer() => playerDunHealth.GetAbilitiesPlayer();

        public int GetMaxHealth() => playerDunHealth.GetMaxHealth();

        public void SetAttackPlayerAnimation() => playerDunHealth.SetAttackPlayerAnimation();

        public Quaternion GetRotationPlayer() => playerDunMove.GetRotationPlayer();
    }
}
