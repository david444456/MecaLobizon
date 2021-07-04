using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BackGround : MonoBehaviour
    {

        public float Scroll = 4;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            rb.velocity = new Vector2(Scroll, 0);
        }
    }
}
