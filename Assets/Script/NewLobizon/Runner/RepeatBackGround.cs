using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    public class RepeatBackGround : MonoBehaviour
    {
        BoxCollider2D Ground;
        public float groundHorizontal;
        public Transform trans;
        public Transform trans2;
        public float Camaraf;
        public int fixDistance;

        // Use this for initialization
        private void Awake()
        {
            Ground = GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            groundHorizontal = Ground.size.x;
        }

        void Reposition()
        {
            //transform.Translate(Vector2.left * (groundHorizontal * 2));
            Vector2 groundOff = new Vector2((groundHorizontal * -2f) + fixDistance, 0);
            transform.position = (Vector2)transform.position + groundOff;
        }

        void Update()
        {
            Camaraf = trans2.position.x + groundHorizontal;
            if (trans.position.x + 4 > Camaraf)
            {
                Debug.Log(trans.position.x + "  " + groundHorizontal);
                Reposition();
                Debug.Log(trans.position.x + "  " + groundHorizontal);
            }

        }
    }
}
