
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ViewWayBoard : MonoBehaviour, IWayBoard, IViewWayBoard
    {
        [Header("Var range of the way")]
        [SerializeField] int _largeOfTheListRow = 4;
        [SerializeField] int _largeOfTheListColumn = 4;

        [Header("Var data in the way")]
        [SerializeField] int[] dataRandomValueInTheWay;

        [Header("Data objects")]
        [SerializeField] GameObject[] slots;

        TypeLootMap actualTypeLootMap;
        List<TypeMap> actualWayTypeMap = new List<TypeMap>();

        List<List<int>> way = new List<List<int>>();
        List<Vector2> realWayToPlayer = new List<Vector2>();

        public void CreatePrincipalWay() {
            dataRandomValueInTheWay = actualTypeLootMap.GetDataRandomValue();
            WayBoard wayBoard = new WayBoard(_largeOfTheListRow, _largeOfTheListColumn, dataRandomValueInTheWay);
            way = wayBoard.GetCalculateNewGameWay();
            realWayToPlayer = wayBoard.GetTheRealWayMovePlayer();
            PrintAllWay();
            AddCorrectTypeMapRealList();
        }

        public void SetTypeLootMap(TypeLootMap newType) => actualTypeLootMap = newType;

        public List<Vector2> GetTheRealWayMovePlayer() => realWayToPlayer;

        public TypeMap GetActualTypeMapByIndex(int index) => actualWayTypeMap[index];

        private void PrintAllWay()
        {
            for (int i = 0; i < _largeOfTheListColumn; i++)
            {
                for (int j = 0; j < way.Count; j++)
                {
                    GameObject gameObjectInstantiate = GetTheCorrectGameObjectToInstantiateWay(way[j][i]);
                    Instantiate(gameObjectInstantiate, new Vector3(transform.position.x + i, 0, transform.position.y + j), Quaternion.identity, gameObject.transform);
                }
            }
        }

        private GameObject GetTheCorrectGameObjectToInstantiateWay(int value)
        {
            switch (value)
            {
                case 0:
                    return CorrectGameObjectAddTypeMap(0);
                case 1:
                    return CorrectGameObjectAddTypeMap(1);
                case 2:
                    return CorrectGameObjectAddTypeMap(2);
                case 3:
                    return CorrectGameObjectAddTypeMap(3);
                case 4:
                    return CorrectGameObjectAddTypeMap(4);
                case 5:
                    return CorrectGameObjectAddTypeMap(5);
                case 6:
                    return CorrectGameObjectAddTypeMap(6);
                default:
                    return CorrectGameObjectAddTypeMap(0);
            }
        }

        private GameObject CorrectGameObjectAddTypeMap(int index) {
            return slots[index];
        }

        private TypeMap GetTheCorrectMap(int index) {
            TypeMap[] typeMaps = actualTypeLootMap.GetTypeMaps();
            for (int i = 0; i < typeMaps.Length; i++) {
                if (typeMaps[i].GetValueInTheWay() == index) {
                    return typeMaps[i];
                }
            }
            return null;
        }

        private void AddCorrectTypeMapRealList() {
            for (int i = 0; i < realWayToPlayer.Count; i++)
            {
                actualWayTypeMap.Add(GetTheCorrectMap(way[(int)realWayToPlayer[i].x][(int)realWayToPlayer[i].y]));
            }           
        }
    }
}
