using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Board.ProgressionCombat;

namespace Board
{
    [CreateAssetMenu(fileName = "Character", menuName = "Data Gungeon/new character dungeon", order = 0)]
    public class CharacterBoard : ScriptableObject
    {
        [SerializeField] int initialLevel = 1;
        [SerializeField] CharacterClass characterClassTypeEnemy;
        [SerializeField] public GameObject prefabGameObject;
        [SerializeField] public int InitialHealthPlayer = 100;
        
        public int GetInitialLevel() => initialLevel;

        public CharacterClass GetCharacterClass() => characterClassTypeEnemy;
    }
}
