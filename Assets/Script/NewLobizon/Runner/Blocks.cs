using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    public class Blocks : MonoBehaviour
    {

        bool alreadyCollisionWithThePlayer = false;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!alreadyCollisionWithThePlayer &&
                (collision.collider.gameObject.name == "PataTrasera" || collision.collider.gameObject.name == "PataDelantera"))
            {
                //NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", 1);
                alreadyCollisionWithThePlayer = true;
            }
        }
    }
}
