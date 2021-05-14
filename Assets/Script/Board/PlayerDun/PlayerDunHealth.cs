using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace Board
{
    public class PlayerDunHealth : MonoBehaviour, IPlayerHealth
    {
        //all system combat

        [SerializeField] AbilitiesCharacter[] abilitiesPlayer;

        CharacterBoard _playerBoard;
        Animator animator;

        private int m_actualCountHealth = 0;

        private void Start()
        {
            animator = GetComponent<Animator>();

            _playerBoard = PlayerData.playerData.GetPlayerBoard();

            SetActualHealthPlayer(PlayerData.playerData.Health);
            print("Eliminated this method after created dontdestroyonload");
        }

        public void ChangePositionPlayerToCombat(Vector3 vector3Position) {
            transform.position = vector3Position;
        }

        public void SetAttackPlayerAnimation() {
            animator.SetTrigger("Attack");
        }

        private void SetHitPlayerAnimation()
        {
            animator.SetTrigger("Hit");
        }

        public AbilitiesCharacter[] GetAbilitiesPlayer() => abilitiesPlayer;

        public void SetActualHealthPlayer(int initialH) {
            m_actualCountHealth = initialH;
        }

        public int GetHealth()
        {
            return m_actualCountHealth;
        }

        public int GetMaxHealth()
        {
            return _playerBoard.InitialHealthPlayer;
        }

        public void SetNewHealth(int value) {
            m_actualCountHealth = m_actualCountHealth - value;
            SetHitPlayerAnimation();
            if (m_actualCountHealth <= 0) {
                animator.SetTrigger("Die");
                print("Perdiste");
            }
        }
    }
}
