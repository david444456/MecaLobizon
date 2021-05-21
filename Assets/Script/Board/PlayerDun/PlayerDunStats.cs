using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class PlayerDunStats : MonoBehaviour
    {
        [SerializeField] float defaultAttackSpeed = 3f;
        [SerializeField] int defaultDamage = 3;

        void Start()
        {
            //inherit data

        }

        public float GetAttackSpeedPlayer() => defaultAttackSpeed;

        public int GetDamageAttackPlayer() => defaultDamage;
    }
}
