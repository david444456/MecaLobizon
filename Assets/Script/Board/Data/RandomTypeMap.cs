using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "TypeMap", menuName = "TypeMap/new random Type Map", order = 0)]
    public class RandomTypeMap : TypeMap
    {
        [Header("Random")]
        [SerializeField] TypeMap[] _randomMaps;

        public TypeMap GetTypeMapRandom() {
            return _randomMaps[0];
        }
    }
}
