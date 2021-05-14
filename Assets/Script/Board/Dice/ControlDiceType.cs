using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ControlDiceType : MonoBehaviour, IControlTypeDice
    {
        [SerializeField] TypeDice typeDiceControlMap;
        [SerializeField] SpriteRenderer[] spriteDiceSite;

        List<int> randomValueListType = new List<int>();
        int m_randomValueToReturnMap = 0;

        private void Awake()
        {
            //pedir dayo type dice
            for (int i = 0; i < typeDiceControlMap.StartMapTypeRandom().Length; i++) {
                randomValueListType.Add(i);
            }

            m_randomValueToReturnMap = GetRandomNumberDifRandomValueToReturnMap();
            spriteDiceSite[0].sprite = typeDiceControlMap.StartMapTypeRandom()[m_randomValueToReturnMap].GetSpriteLoot();

            print(m_randomValueToReturnMap + " " + typeDiceControlMap.StartMapTypeRandom()[m_randomValueToReturnMap].name);
            for (int i = 1; i < spriteDiceSite.Length; i++) {
                spriteDiceSite[i].sprite = typeDiceControlMap.StartMapTypeRandom()[GetRandomNumberDifRandomValueToReturnMap()].GetSpriteLoot();
            }
        }

        public TypeLootMap GetMapTypeRandom() {
            return typeDiceControlMap.StartMapTypeRandom()[m_randomValueToReturnMap];
        }

        public void SetTypeDice(TypeDice newDice) {
            typeDiceControlMap = newDice;
        }

        private int GetRandomNumberDifRandomValueToReturnMap() {
            int newValue = Random.Range(0, randomValueListType.Count);
            int newValueToReturn = randomValueListType[newValue]; ;
            randomValueListType.Remove(newValueToReturn);

            return newValueToReturn;
        }
    }
}
