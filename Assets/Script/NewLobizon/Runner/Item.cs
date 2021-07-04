using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    public class Item : MonoBehaviour
    {

        public int pointsToIncrement = 10;
        public AudioClip itemAudio;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                //NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                AudioSource.PlayClipAtPoint(itemAudio, transform.position, 1f);
                Debug.Log("Call to pool");
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }

        }
    }
}
