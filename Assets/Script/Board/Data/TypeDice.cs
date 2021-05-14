using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "Dice", menuName = "Data Gungeon/new type dice", order = 0)]
    public class TypeDice : ScriptableObject
    {
        [SerializeField] TypeLootMap[] _typesLootsMap;

        public TypeLootMap[] StartMapTypeRandom()
        {
            return _typesLootsMap;
        }
    }
}
