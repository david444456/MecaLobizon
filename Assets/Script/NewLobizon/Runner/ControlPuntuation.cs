using UnityEngine;
using System.Collections;
using Lobizon.Core;

namespace Lobizon.Runner
{
    public class ControlPuntuation : MonoBehaviour
    {

        public int puntuation = 0;
        public int puntuationItem = 0;
        public int pointsGoals = 100;
        public bool ItemOPoints;
        public TextMesh text;
        public TextMesh textItem;
        public TextMesh textGoals;

        public GameObject GameOver;
        public TextMesh Record;
        public TextMesh Total;

        public int numsLevelComplete;

        bool puntuacionObjetivoBool = true;

        void Start()
        {
            //NotificationCenter.DefaultCenter().AddObserver(this, "IncrementarPuntos");
            // NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeMuerto");
            UpdateActualScore();
        }

        void Update()
        {
            if (ItemOPoints)
            {
                if (pointsGoals <= puntuation && puntuacionObjetivoBool)
                {
                    Debug.Log("pasaste el nivel");
                    completeLevel();
                    puntuacionObjetivoBool = false;
                }
            }
            else
            {
                if (pointsGoals <= puntuationItem && puntuacionObjetivoBool)
                {
                    Debug.Log("pasaste el nivel");
                    completeLevel();
                    puntuacionObjetivoBool = false;
                }
            }
        }

        void AugmentPoints(int points)
        {
            int puntosAIncrementar = points;
            puntuation += puntosAIncrementar;

            UpdateActualScore();
        }

        void UpdateActualScore()
        {
            text.text = puntuation.ToString();
            textItem.text = puntuationItem.ToString();
            textGoals.text = pointsGoals.ToString();
        }

        private void completeLevel()
        {
            GameOver.SetActive(true);
            GetComponent<AudioSource>().Stop();
            Total.text = puntuation.ToString();
        }
    }
}

