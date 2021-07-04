using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    public class BlockGenerator : MonoBehaviour
    {
        public GameObject[] obj;
        public float tiempoMin = 1f;
        public float tiempoMax = 3f;

        void Start()
        {
            //NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");
        }

        private void PlayerStartRunning()
        {
            StartCoroutine(GenerateObjects());
        }

        IEnumerator GenerateObjects()
        {
            Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));
            StartCoroutine(GenerateObjects());
        }
    }
}
