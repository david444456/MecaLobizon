using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PrincipalMap
{
    public class PlayerArgMove : PlayerMove
    {
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

            LastPositionToMove = transform.position;
        }

        public override void Update()
        {
            if (!newMovement) return;

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
                print("El condicional del update" + (navMeshAgent.remainingDistance));
                newMovement = false;
                UpdateAnimatorPlayer();
                controlGameMap.FinishPath();
            }
        }

        public override void NewMovementPlayer(int value)
        {
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
