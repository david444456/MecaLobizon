using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public interface IViewWayBoard
    {
        TypeMap GetActualTypeMapByIndex(int index);
        void SetTypeLootMap(TypeLootMap newType);
        void CreatePrincipalWay(int row, int column);
    }
}
