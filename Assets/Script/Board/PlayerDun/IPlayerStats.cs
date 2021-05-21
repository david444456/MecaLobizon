using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public interface IPlayerStats 
    {
        float GetAttackSpeedPlayer();
        int GetDamageAttackPlayer();
    }
}
