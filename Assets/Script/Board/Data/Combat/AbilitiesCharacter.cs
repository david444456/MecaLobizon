using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "Ab 1", menuName = "Board/new abilities combat", order = 0)]
    public class AbilitiesCharacter : ScriptableObject
    {
        public float timeToAttack = 5;
        public int damage = 10;
        public string nameAbilities = "Palo de escoba";
        public Sprite spriteAbilities;
    }
}
