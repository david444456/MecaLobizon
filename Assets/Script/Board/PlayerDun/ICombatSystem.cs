
using UnityEngine;

namespace Board
{
    public interface ICombatSystem
    {
        void NewCombatTypeCharacter(Vector2 lastPosition, GameObject prefabEnemy, CharacterBoard characterBoardEnemy);
        void TakeDamageEnemy();
        void TakeDamagePlayer();
        bool GetActualStateCombat();

    }

    public interface IAnimationDeadPlayer {
        void EventAnimationDiePlayer();
    }
}
