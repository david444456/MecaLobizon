using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class PlayerDunMove : PlayerMove, IPlayerMove
    {

        public override void Start()
        {
            base.Start();
            StartDataInTheGame(MediatorBoard.Mediator.GetTheRealWayMovePlayer());

            StartCoroutine(StartGameMove());
        }

        public override void Update()
        {
            if (!stopMoveAroundBoard)
            {
                UpdateTransformPositionXZ();
            }
        }

        public override void CompletePathMove() {
            newMovement = false;

            // UpdateAnimatorPlayer();

            MediatorBoard.Mediator.CompletePathPlayer(m_actualIndexList);
        }

        public void ChangeMoveStatePlayer(bool newState) {
            stopMoveAroundBoard = newState;
        }

        public void ContinueMovePlayerAfterRound() {
            stopMoveAroundBoard = false;
        }

        public Vector2 GetPositioByIndex(int index) {
            return realWayToPlayer[index];
        }

        public Quaternion GetRotationPlayer() {
            return transform.rotation;
        }

        public override void UpdateAnimatorPlayer()
        {
            animator.SetTrigger("MoveTo");
        }

        public override void CompleteRoundWay()
        {
            base.CompleteRoundWay();
            MediatorBoard.Mediator.ExitBoardEvent();
        }

        public override void VerificatedRotationPlayer() {
            int firstNumer = m_actualIndexList - 2;
            int secondNumer = m_actualIndexList;
            int betweenUs = m_actualIndexList - 1;

            if (m_actualIndexList == 0)
            {
                firstNumer = realWayToPlayer.Count - 2;
                betweenUs = realWayToPlayer.Count - 1;
            }
            else if(m_actualIndexList == 1){
                firstNumer = realWayToPlayer.Count - 1;
                betweenUs = 0;
            }

            if (realWayToPlayer[firstNumer].x != realWayToPlayer[secondNumer].x &&
                realWayToPlayer[firstNumer].y != realWayToPlayer[secondNumer].y)
            {
                float rotationInY = 0;

                if (realWayToPlayer[betweenUs].x == realWayToPlayer[secondNumer].x && realWayToPlayer[betweenUs].y > realWayToPlayer[secondNumer].y)
                {
                    rotationInY = 180;
                    print("Izquierda");
                }
                else if (realWayToPlayer[betweenUs].x == realWayToPlayer[secondNumer].x && realWayToPlayer[betweenUs].y < realWayToPlayer[secondNumer].y)
                {
                    rotationInY = 0;
                    print("Derecha");
                }
                else if (realWayToPlayer[betweenUs].y == realWayToPlayer[secondNumer].y && realWayToPlayer[betweenUs].x < realWayToPlayer[secondNumer].x)
                {
                    rotationInY = -90;
                    print("Arriba");
                }
                else if (realWayToPlayer[betweenUs].y == realWayToPlayer[secondNumer].y && realWayToPlayer[betweenUs].x > realWayToPlayer[secondNumer].x)
                {
                    rotationInY = 90;
                    print("Abajo");
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, rotationInY, 0));

                print("Girar");
            }
        }

        private IEnumerator StartGameMove() {
            yield return new WaitForSeconds(1);
            StartCoroutine(MovementDicePlayer());
        }
    }
}
