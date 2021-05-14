using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ControlSlotEvent : MonoBehaviour, IControlSlotMapEvent
    {
        [SerializeField] float timeWaitEvent = 1f;
        private TypeMap typeMap;

        public void StartNewEventMap(TypeMap newTypeMap)
        {
            typeMap = newTypeMap;
            print("New event");
        }

        public void FinishEvent() {
            print("Termina el : " + typeMap.name);
            StartCoroutine(CompleteEventPlayer());
        }

        IEnumerator CompleteEventPlayer() {
            yield return new WaitForSeconds(timeWaitEvent);
            MediatorBoard.Mediator.CompleteEventPlayer();
        }
    }
}
