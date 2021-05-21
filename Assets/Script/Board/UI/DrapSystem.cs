using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Board
{
    public class DrapSystem : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] Canvas PrincipalCanvas;
        

        RectTransform rectTransform;
        CanvasGroup canvasGroup;
        HorizontalLayoutGroup HorizontalGroup;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            HorizontalGroup = GetComponentInParent<HorizontalLayoutGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            print("Begin");
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            print("OnDrag");
            rectTransform.anchoredPosition += eventData.delta/ PrincipalCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            print("End");

            //ray slot detection
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                print(hit.transform.position);
                hit.transform.gameObject.SetActive(false);
            }

            //
            UpdateHorizontalGroup();
            StartCoroutine(ActiveBlockRayCast());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            print("Down");
        }

        IEnumerator ActiveBlockRayCast() {
            yield return new WaitForSeconds(0.1f);
            canvasGroup.blocksRaycasts = true;
        }

        private void UpdateHorizontalGroup() {
            RectOffset tempPadding = new RectOffset(
                HorizontalGroup.padding.left,
                HorizontalGroup.padding.right,
                HorizontalGroup.padding.top,
                HorizontalGroup.padding.bottom);

            HorizontalGroup.padding = tempPadding;
        }
    }
}
