
using UnityEngine;

namespace Board
{
    public interface IPlayerHealth
    {
        int GetHealth();
        void SetNewHealth(int value);
        void ChangePositionPlayerToCombat(Vector3 vector3Position);
         AbilitiesCharacter[] GetAbilitiesPlayer();
        int GetMaxHealth();
        void SetAttackPlayerAnimation();
    }
}
