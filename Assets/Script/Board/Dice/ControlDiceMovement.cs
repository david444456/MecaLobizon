using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ControlDiceMovement : MonoBehaviour, IDiceMovement
    {
        [SerializeField] int maxNumberRoll = 6;

        public int GetNewDiceRoll()
        {
            return Random.Range(1, maxNumberRoll + 1);
        }
    }
}
