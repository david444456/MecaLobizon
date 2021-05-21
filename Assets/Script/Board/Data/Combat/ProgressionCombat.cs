using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board {
    [CreateAssetMenu(fileName = "Prog 1", menuName = "Board/new prog combat", order = 0)]
    public class ProgressionCombat : ScriptableObject
    {
        [SerializeField] public int largeOfTheListRow = 4;
        [SerializeField] public int largeOfTheListColumn = 4;
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookUpTable[characterClass][stat];

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }

            return levels[level - 1];
        }

        public AbilitiesCharacter[] GetAbilitiesCharacters(CharacterClass characterClass) {
            for (int i = 0; i < characterClasses.Length; i++) {
                if (characterClass == characterClasses[i].character) {
                    return characterClasses[i].abilitiesCharacters;
                }
            }
            return null;
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookUpTable[characterClass][stat];
            return levels.Length;

        }

       /* public string GetStringDead(CharacterClass characterClass)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.character == characterClass)
                {
                    return progressionClass.textKilledYou;
                }

            }
            return null;
        }*/

        private void BuildLookup()
        {
            if (lookUpTable != null) return;

            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookUpTable[progressionClass.character] = statLookupTable;
            }

        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass character;
            public AbilitiesCharacter[] abilitiesCharacters;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }


        public enum CharacterClass
        {
            Lobizon,
            Ekeko,
            Llorona,
            Pombero
        }

        public enum Stat
        {
            Health,
            AttackSpeed,
            RewardCoin,
            Damage
        }

    }
}

