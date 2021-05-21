using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public interface IControlEnemySpawn 
    {
        CharacterBoard GetSlotEnemy(int indexWay);
        GameObject GetActualInstanceEnemy(int index);
    }
}
