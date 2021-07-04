using UnityEngine;
using System.Collections;

namespace Lobizon.Runner
{
    public class Scroll : MonoBehaviour
    {

        public bool startMovement = false;
        public float velocityMove = 0f;


        private bool inMovement = false;
        private float timeStart = 0f;
        Renderer rendererBackGround;

        private void Awake()
        {
            rendererBackGround = GetComponent<Renderer>();
        }

        void Start()
        {
            //NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");
            //NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
            if (startMovement)
            {
                PlayerStartMovement();
            }
        }

        void Update()
        {
            if (inMovement)
            {
                rendererBackGround.material.mainTextureOffset = new Vector2(((Time.time - timeStart) * velocityMove) % 1, 0);
            }
        }

        void PlayerDead()
        {
            inMovement = false;
        }

        void PlayerStartMovement()
        {
            inMovement = true;
            timeStart = Time.time;
        }
    }
}
