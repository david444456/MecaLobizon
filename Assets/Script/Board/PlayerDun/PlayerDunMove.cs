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
        }

        public override void Update()
        {
            UpdateTransformPositionXZ();
        }

        public override void CompletePathMove() {
            newMovement = false;

           // UpdateAnimatorPlayer();

            MediatorBoard.Mediator.CompletePathPlayer(m_actualIndexList);
        }

        public Vector2 GetPositioByIndex(int index) {
            return realWayToPlayer[index];
        }

        public override void NewMovementPlayer(int value)
        {
            newMovement = true;

            //UpdateAnimatorPlayer();

            m_countMaxMoveDice = value;
            m_countMoveDice = 0;
            StartCoroutine(MovementDicePlayer());
        }

        public override void UpdateAnimatorPlayer()
        {
            animator.SetTrigger("MoveTo");
        }

        public override void CompleteRoundWay()
        {
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
                if (realWayToPlayer[betweenUs].x == realWayToPlayer[secondNumer].x && realWayToPlayer[betweenUs].y > realWayToPlayer[secondNumer].y)
                    print("Izquierda");
                else if (realWayToPlayer[betweenUs].x == realWayToPlayer[secondNumer].x && realWayToPlayer[betweenUs].y < realWayToPlayer[secondNumer].y)
                    print("Derecha");
                else if(realWayToPlayer[betweenUs].y == realWayToPlayer[secondNumer].y && realWayToPlayer[betweenUs].x < realWayToPlayer[secondNumer].x)
                    print("Arriba");
                else if (realWayToPlayer[betweenUs].y == realWayToPlayer[secondNumer].y && realWayToPlayer[betweenUs].x > realWayToPlayer[secondNumer].x)
                    print("Abajo");

                print("Girar");
                Debug.Break();
            }
        }
    }
}
