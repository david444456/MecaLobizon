
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "TypeMap", menuName = "TypeMap/new resources Type Map", order = 0)]
    public class ResourcesTypeMap : TypeMap
    {
        [Header("Resources")]
        [SerializeField] int _coinsToReward = 0;

        public int GetCoinsReward() => _coinsToReward;
    }
}