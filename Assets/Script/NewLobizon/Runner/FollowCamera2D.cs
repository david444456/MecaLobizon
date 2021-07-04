using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    public class FollowCamera2D : MonoBehaviour
    {

        public Transform FollowTransform;
        public float spaceBetween = 6f;
        AudioSource audio;

        void Update()
        {
            transform.position = new Vector3(FollowTransform.position.x + spaceBetween, transform.position.y, transform.position.z);
        }
    }
}
