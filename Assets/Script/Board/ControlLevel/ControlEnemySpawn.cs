using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class ControlEnemySpawn : MonoBehaviour, IControlEnemySpawn
    {
        [SerializeField] CharacterBoard characterBoardEnemyInitial;
        [SerializeField] int minSpawnEnemyInBoard = 2;
        [SerializeField] int maxSpawnEnemyInBoard = 6;
        [SerializeField] float positionSpawnY = 0.54f;

        List<Vector2> realWayPlayer;
        List<CharacterBoard> wayEnemysSlots = new List<CharacterBoard>();
        Dictionary<Vector2, CharacterBoard> enemyCharData = new Dictionary<Vector2, CharacterBoard>();
        Dictionary<Vector2, GameObject> enemyPrefabData = new Dictionary<Vector2, GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            realWayPlayer = MediatorBoard.Mediator.GetTheRealWayMovePlayer();
            SpawnNewEnemysInSlots();
        }

        private void SpawnNewEnemysInSlots() {
            int index = Random.Range(minSpawnEnemyInBoard, maxSpawnEnemyInBoard);

            for (int i = 0; i <= index; i++) {
                int indexWay = Random.Range(1, realWayPlayer.Count);

                CharacterBoard characterBoardCheck = characterBoardEnemyInitial;

                if (!enemyCharData.TryGetValue(realWayPlayer[indexWay], out characterBoardCheck))
                {
                    enemyCharData.Add(realWayPlayer[indexWay], characterBoardEnemyInitial);
                    SpawnNewEnemy(characterBoardEnemyInitial, new Vector3(realWayPlayer[indexWay].y, positionSpawnY, realWayPlayer[indexWay].x), indexWay);
                }
                else {
                    print("Same enemy");
                }
            }
        }

        public CharacterBoard GetSlotEnemy(int indexWay) {
            CharacterBoard characterBoardCheck = characterBoardEnemyInitial;

            if (enemyCharData.TryGetValue(realWayPlayer[indexWay], out characterBoardCheck)) {
                return characterBoardCheck;
            }
            return null;
        }

        public GameObject GetActualInstanceEnemy(int index) {

            GameObject gameObjectInstantiate;

            if (enemyPrefabData.TryGetValue(realWayPlayer[index], out gameObjectInstantiate))
            {
                return gameObjectInstantiate;
            }
            return null;
        }

        private void SpawnNewEnemy(CharacterBoard newCharacterBoard, Vector3 position, int index) {
            // GameObject gameObjectInstantiate = GetTheCorrectGameObjectToInstantiateWay(way[j][i]);
            GameObject gameObjectInstantiate = Instantiate(newCharacterBoard.prefabGameObject, position, Quaternion.identity, gameObject.transform);
            enemyPrefabData.Add(realWayPlayer[index], gameObjectInstantiate);
        }
    
    }
}
