using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public abstract class PlayerMove : MonoBehaviour
    {
        [SerializeField] public float timeSpeedToMove = 1;
        [SerializeField] public float _distanceUpPlayerInTheWay = 0.5f;
        [SerializeField] public float _deltaMoveUpdate = 0.01f;

        [HideInInspector] public List<Vector2> realWayToPlayer = new List<Vector2>();

        [HideInInspector] public Vector3 LastPositionToMove;
        [HideInInspector] public int m_actualIndexList = 0;

        //dice move
        [HideInInspector] public int m_countMaxMoveDice = 0;
        [HideInInspector] public int m_countMoveDice = 0;

        public bool newMovement = false;
        public bool stopMoveAroundBoard = false;
        public Animator animator;

        public virtual void Start() {
            animator = GetComponent<Animator>();
        }

        public virtual void Update() {

        }

        public void StartDataInTheGame(List<Vector2> newList)
        {
            realWayToPlayer = newList;
            transform.position = new Vector3(realWayToPlayer[0].y, _distanceUpPlayerInTheWay, realWayToPlayer[0].x);
            LastPositionToMove = transform.position;
        }

        public IEnumerator MovementDicePlayer()
        {
            yield return new WaitForSeconds(timeSpeedToMove);

            if (stopMoveAroundBoard)
            {
               // yield return new WaitForSeconds(timeSpeedToMove * 2);
                print("Estoy deteniendo el movimiento");
                StartCoroutine(MovementDicePlayer());
            }
            else
            {
                print("Estoy moviendo " + stopMoveAroundBoard);
                m_countMoveDice++;
                //move

                if (realWayToPlayer.Count > m_actualIndexList + 1)
                {
                    m_actualIndexList++;
                }
                else
                {
                    CompleteRoundWay();
                    m_actualIndexList = 0;
                }

                VerificatedRotationPlayer();
                LastPositionToMove = new Vector3(realWayToPlayer[m_actualIndexList].y, _distanceUpPlayerInTheWay, realWayToPlayer[m_actualIndexList].x);


                //                
                CompletePathMove();


                print("Voy hacia: " + realWayToPlayer[m_actualIndexList].y + " z: " + realWayToPlayer[m_actualIndexList].x);

                if (!stopMoveAroundBoard) {
                    UpdateAnimatorPlayer();
                }

                //repeting cycle
                StartCoroutine(MovementDicePlayer());
            }
        }

        public virtual void CompletePathMove() {

        }

        public void UpdateTransformPositionXZ()
        {
            if (transform.position != LastPositionToMove)
            {

                transform.position = Vector3.MoveTowards(transform.position, LastPositionToMove, _deltaMoveUpdate);
            }
        }

        public virtual void UpdateAnimatorPlayer()
        {
            
        }

        public virtual void VerificatedRotationPlayer() {

        }

        public virtual void CompleteRoundWay() {
            stopMoveAroundBoard = true;
        }
    }
}
