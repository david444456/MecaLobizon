using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Board
{
    public class Slot3DDragDetection : MonoBehaviour
    {
        private void OnMouseDown()
        {
            //print("MDown");
        }

        private void OnMouseUpAsButton()
        {
           // print("ButtonAsButton");

        }

        private void OnMouseDrag()
        {
           // print("DRAG");
        }

        private Vector3 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
