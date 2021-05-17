using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincipalMap {
    [CreateAssetMenu(fileName = "RegionEvent", menuName = "Principal/new Group Regional event", order = 0)]
    public class GroupRegionalEvent : ScriptableObject
    {
        [SerializeField] RegionalEvent[] regionalEvent;

        public int internCountQueueRegionalEvent = 0;

        public RegionalEvent[] GetRegionalEvents() => regionalEvent;

        public RegionalEvent GetRandomRegionalEvent()
        {
            return regionalEvent[Random.Range(0, regionalEvent.Length)];

            int newValue = internCountQueueRegionalEvent;

            if (internCountQueueRegionalEvent < regionalEvent.Length-1)
            {
                internCountQueueRegionalEvent++;
            }
            else {
                internCountQueueRegionalEvent = 0;
            }

            return regionalEvent[newValue];
        }
    }
}
