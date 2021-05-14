using Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData playerData;

        [SerializeField] CharacterBoard _playerBoard;

        int health = 0;
        int coin = 0;
        int damage = 0;
        int move = 0;

        public int Health { get => health;
            set {

            }
        }

        public int Coin
        {
            get => coin;
            set
            {

            }
        }

        public int Damage { get => damage;
            set {

            }
        }

        public int Move
        {
            get => move;
            set
            {

            }
        }

        void Awake()
        {
            if (playerData == null)
                playerData = this;
            else
                Destroy(gameObject);

            health = _playerBoard.InitialHealthPlayer;
        }

        void Start()
        {

        }

        public CharacterBoard GetPlayerBoard() => _playerBoard;

    }
}
