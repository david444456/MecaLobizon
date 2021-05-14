using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "TypeMap", menuName = "TypeMap/new combat type map", order = 0)]
    public class CombatTypeMap : TypeMap
    {
        [SerializeField]
        GameObject _enemyPrefabBoard = null;

        public GameObject GetTypeEnemy() => _enemyPrefabBoard;
    }
}
