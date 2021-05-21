using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class EnemyControl : MonoBehaviour
    { 
        int actualHealth = 0;
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateDataEnemy(int maxHealth) {
            actualHealth = maxHealth;
        }

        public void TakeDamage(int damage) {
            actualHealth -= damage;
        }
    }
}
