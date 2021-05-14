using Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData playerData;

        [SerializeField] CharacterBoard _playerBoard;

        public ProgressionCombat progressionCombat;
        public CharacterBoard characterBoardEnemy;

        int health = 0;
        int coin = 0;
        int damage = 0;
        int move = 0;

        public int Health { get => health;
            set {
                health = value;
            }
        }

        public int Coin
        {
            get => coin;
            set
            {
                coin = value;
            }
        }

        public int Damage { get => damage;
            set {
                damage = value;
            }
        }

        public int Move
        {
            get => move;
            set
            {
                move = value;
            }
        }

        void Awake()
        {
            if (playerData == null)
                playerData = this;
            else
                Destroy(gameObject);

            health = _playerBoard.InitialHealthPlayer;

            DontDestroyOnLoad(this);
        }

        void Start()
        {

        }

        public CharacterBoard GetPlayerBoard() => _playerBoard;

        public void SetAugmentCoin(int newCoin) {
            coin += newCoin;
        }

        public void LoadScene(int index) {
            SceneManager.LoadScene(index);
        }
    }
}
