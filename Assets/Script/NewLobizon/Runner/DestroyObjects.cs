using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner {
    public class DestroyObjects : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!(other.tag == "BackGround"))
            {
                if (other.tag == "Player")
                {
                    DeadPlayer();
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }

        }

        public void DeadPlayer()
        {
            //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeMuerto");
            print("Dead player with limits ");

        }
    }

}
