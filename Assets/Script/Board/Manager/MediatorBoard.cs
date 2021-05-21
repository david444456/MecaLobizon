using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class MediatorBoard : MonoBehaviour, IViewWayBoard, IWayBoard, IDiceMovement, IPlayerMove, ICombatSystem,
        IPlayerHealth, IControlSlotMapEvent, IControlCoinInGame, ISystemBoard, IControlTypeDice, IInternalSystemBoard,
        IControlEnemySpawn, IPlayerStats, IAnimationDeadPlayer
    {
        public static MediatorBoard Mediator;

        [Header("Class")]
        [SerializeField] ViewWayBoard viewWayBoard;
        [SerializeField] PlayerDunMove playerBoardMove;
        [SerializeField] ControlDiceMovement ctrolDiceMove;
        [SerializeField] CombatSystem combatSystem;
        [SerializeField] PlayerDunHealth playerDunHealth;
        [SerializeField] PlayerDunStats playerDunStats;
        [SerializeField] ControlSlotEvent slotEvent;
        [SerializeField] ControlCoinInGame coinInGame;
        [SerializeField] ViewSystemBoard viewSystemBoard;
        [SerializeField] ControlDiceType controlDiceType;
        [SerializeField] InternalSystemBoard internalSystemBoard;
        [SerializeField] ControlEnemySpawn controlEnemySpawn;

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

        //view system
        public TypeMap GetActualTypeMapByIndex(int index) => viewWayBoard.GetActualTypeMapByIndex(index);

        public void SetTypeLootMap(TypeLootMap newType) => viewWayBoard.SetTypeLootMap(newType);

        public void CreatePrincipalWay(int row, int column) => viewWayBoard.CreatePrincipalWay(row, column);

        public List<Vector2> GetTheRealWayMovePlayer() => viewWayBoard.GetTheRealWayMovePlayer();

        public void ExitBoardEvent() => viewSystemBoard.ExitBoardEvent();

        public void LostTheGame() => viewSystemBoard.LostTheGame();

        //dice
        public int GetNewDiceRoll() => ctrolDiceMove.GetNewDiceRoll();

        //combat
        public void NewCombatTypeCharacter(Vector2 lastPosition, GameObject prefabEnemy, CharacterBoard characterBoardEnemy) => combatSystem.NewCombatTypeCharacter(lastPosition, prefabEnemy, characterBoardEnemy);

        public void TakeDamageEnemy() => combatSystem.TakeDamageEnemy();

        public void TakeDamagePlayer() => combatSystem.TakeDamagePlayer();

        public bool GetActualStateCombat() => combatSystem.GetActualStateCombat();

        //health
        public int GetHealth() => playerDunHealth.GetHealth();

        public void SetNewHealth(int value) => playerDunHealth.SetNewHealth(value);

        public void ChangePositionPlayerToCombat(Vector3 vector3Position) => playerDunHealth.ChangePositionPlayerToCombat(vector3Position);

        public List<AbilitiesCharacter> GetAbilitiesPlayer() => playerDunHealth.GetAbilitiesPlayer();

        public int GetMaxHealth() => playerDunHealth.GetMaxHealth();

        public void SetAttackPlayerAnimation() => playerDunHealth.SetAttackPlayerAnimation();

        //event map
        public void StartNewEventMap(TypeMap typeMap) => slotEvent.StartNewEventMap(typeMap);

        //control
        public int GetActualCoin() => coinInGame.GetActualCoin();

        public void SetCoinRewardEvent(int count) => coinInGame.SetCoinRewardEvent(count);

        public TypeLootMap GetMapTypeRandom() => controlDiceType.GetMapTypeRandom();

        //internal
        public void CompletePathPlayer(int index) => internalSystemBoard.CompletePathPlayer(index);

        public void CompleteEventPlayer() => internalSystemBoard.CompleteEventPlayer();

        public ProgressionCombat GetProgressionCombat() => internalSystemBoard.GetProgressionCombat();

        public void EventAnimationDiePlayer() => internalSystemBoard.EventAnimationDiePlayer();

        //player move
        public Vector2 GetPositioByIndex(int index) => playerBoardMove.GetPositioByIndex(index);

        public void ChangeMoveStatePlayer(bool newState) => playerBoardMove.ChangeMoveStatePlayer(newState);

        public Quaternion GetRotationPlayer() => playerBoardMove.GetRotationPlayer();

        //spawn
        public CharacterBoard GetSlotEnemy(int indexWay) => controlEnemySpawn.GetSlotEnemy(indexWay);

        public GameObject GetActualInstanceEnemy(int index) => controlEnemySpawn.GetActualInstanceEnemy(index);

        //stats
        public float GetAttackSpeedPlayer() => playerDunStats.GetAttackSpeedPlayer();

        public int GetDamageAttackPlayer() => playerDunStats.GetDamageAttackPlayer();

    }
}
