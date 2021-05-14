using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public abstract class TypeMap : ScriptableObject
    {
        [Header("TypeMap")]
        [SerializeField] int _dataValueInTheWay = 0;
        [SerializeField] GameObject prefabBackGroundInTheWay = null;
        [SerializeField] TypeEventBoard typeEventBoard;

        public int GetValueInTheWay() => _dataValueInTheWay;

        public GameObject GetPrefabBackgroundInthwWay() => prefabBackGroundInTheWay;

        public TypeEventBoard GetTypeEventBoard() => typeEventBoard;

    }

    public enum TypeEventBoard
    {
        Combat,
        Event,
        Exit
    }
}
