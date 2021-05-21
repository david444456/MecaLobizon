using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Board
{
    public class CardSlotDrapDetection : MonoBehaviour, IDropHandler
    {
        [SerializeField] Vector2 newAnchorerPositionCard = new Vector2(-15, -20);

        public void OnDrop(PointerEventData eventData)
        {
            print("Drop");
            if (eventData.pointerDrag != null) {
                eventData.pointerDrag.transform.SetParent(this.transform);
                eventData.pointerDrag.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newAnchorerPositionCard;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }
    }
}
