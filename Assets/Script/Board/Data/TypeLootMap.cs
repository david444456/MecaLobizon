using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "LootMap", menuName = "Data Gungeon/new type loot map", order = 0)]
    public class TypeLootMap : ScriptableObject
    {
        [SerializeField] TypeMap[] typeMaps;
        [SerializeField] string nameTypeLootMap;
        [SerializeField] Sprite spriteTypeLoot;
        [SerializeField] int[] dataRandomValueInTheWay;

        public TypeMap[] GetTypeMaps() => typeMaps;

        public string GetNameLootMap() => nameTypeLootMap;

        public Sprite GetSpriteLoot() => spriteTypeLoot;

        public int[] GetDataRandomValue() => dataRandomValueInTheWay;
    }
}