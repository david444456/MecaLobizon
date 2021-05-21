using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PrincipalMap
{
    public class PlayerArgMove : PlayerMove
    {
        public bool DiePlayer = false;

        [Header("Argentina move")]
        [SerializeField] ControlGameMap controlGameMap;
        [SerializeField] GameObject[] gameObjectsSlotsToMove;
        [SerializeField] float pathEndThreshold = 0.1f;
        [SerializeField] AudioSource audioSource;
        

        NavMeshAgent navMeshAgent;
        NavMeshPath path;

        public override void Start()
        {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();



            for (int i = 0; i < gameObjectsSlotsToMove.Length; i++) {
                Vector2 vector2 = new Vector2(gameObjectsSlotsToMove[i].transform.position.x, gameObjectsSlotsToMove[i].transform.position.z);
                realWayToPlayer.Add(vector2);
            }

            navMeshAgent.enabled = false;
            transform.position = new Vector3(realWayToPlayer[PlayerData.playerData.lastIndexPositionInMap].x, _distanceUpPlayerInTheWay, realWayToPlayer[PlayerData.playerData.lastIndexPositionInMap].y);
            navMeshAgent.enabled = true;

            LastPositionToMove = transform.position;
        }

        public override void Update()
        {
            if (!newMovement) return;

            if (DiePlayer) {
                navMeshAgent.isStopped = true;
                audioSource.Stop();
            }

            /*move
            if (transform.position != LastPositionToMove)
                transform.position = Vector3.MoveTowards(transform.position, LastPositionToMove, _deltaMoveUpdate);
            else {
                
            }*/

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + pathEndThreshold)
            {
                if (Vector3.Distance(LastPositionToMove, transform.position) <= navMeshAgent.stoppingDistance + pathEndThreshold)
                {
                    print("El otro condicional del update del playerargmove");
                }

                audioSource.Stop();
                newMovement = false;
                UpdateAnimatorPlayer();
                controlGameMap.FinishPath();
            }
        }

        public void NewMovementPlayer(int value)
        {
            PlayerData.playerData.lastIndexPositionInMap = value;
            LastPositionToMove = new Vector3( realWayToPlayer[value].x, _distanceUpPlayerInTheWay, realWayToPlayer[value].y);

            if (navMeshAgent.enabled) navMeshAgent.SetDestination(LastPositionToMove);

            StartCoroutine(timeToTrue());

        }

        public override void UpdateAnimatorPlayer()
        {
             animator.SetBool("Move", newMovement);
        }

        IEnumerator timeToTrue() {
            yield return new WaitForSeconds(0.1f);
            newMovement = true;
            audioSource.Play();

            UpdateAnimatorPlayer();
        }

    }
}
