using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using static Board.ProgressionCombat;

namespace Board
{
    public class CharacterAnimation : MonoBehaviour, IAnimationDeadPlayer
    {
        [SerializeField] CharacterClass characterClass;

        public void TakeDamage() {
            if (characterClass == CharacterClass.Lobizon)
            {
                MediatorBoard.Mediator.TakeDamageEnemy();
                
            }
            else {
                MediatorBoard.Mediator.TakeDamagePlayer();
            }
        }

        public void EventAnimationDiePlayer() {
            if (characterClass == CharacterClass.Lobizon) {
                MediatorBoard.Mediator.EventAnimationDiePlayer();
            }
        }
    }
}
