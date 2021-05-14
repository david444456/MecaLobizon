using PrincipalMap;
using Board;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace General
{
    public abstract class View : MonoBehaviour
    {
        [Header("Dice")]
        [SerializeField] public GameObject UIDiceMove;
        [SerializeField] public GameObject UIButtonRollDice;
        [SerializeField] public GameObject UIButtonConfirmRoll;
        [SerializeField] public Text textUIDiceMove;
        [SerializeField] public Image imageDice;
        [SerializeField] public Sprite[] spritesDice;
        [SerializeField] public float timeToChangeImageRandomDice = 0.1f;

        [Header("Event")]
        [SerializeField] public GameObject UIEventShow;
        [SerializeField] public Text textUIEventShow; 

        [HideInInspector] public int m_lastDiceMoveValue = 0;
        [HideInInspector] public bool m_rollMoment = false;

        [HideInInspector] public float m_countTime = 0;

        public virtual void Update() {
            if (!m_rollMoment) return;
            m_countTime += Time.deltaTime;
            if (m_countTime > timeToChangeImageRandomDice)
            {
                imageDice.sprite = spritesDice[Random.Range(0, spritesDice.Length)];
                m_countTime = 0;
            }
        }

        public void ActiveButtonDiceNewRollMove() => UIButtonRollDice.SetActive(true);

        public void ActiveNewRollMovement()
        {
            textUIDiceMove.text = "0";
            UIDiceMove.SetActive(true);
        }

        public virtual void SetNewRandomDiceMove()
        {

        }

        public void StartNewEventMap(TypeMap typeMap)
        {
            UIEventShow.SetActive(true);
            //text
        }

        public void SetCoinToShowInUI(int coinToShow)
        {
            print("show : " + coinToShow);
        }

        public virtual void SetNewMovementPlayer() //button
        {
            //internalSystemBoard.SetMovementPlayer(m_lastDiceMoveValue);
        }

        public IEnumerator RollDiceMove()
        {
            m_rollMoment = true;
            yield return new WaitForSeconds(1.2f);
            textUIDiceMove.text = m_lastDiceMoveValue.ToString();
            m_rollMoment = false;

            imageDice.sprite = spritesDice[m_lastDiceMoveValue - 1];
            UIButtonConfirmRoll.SetActive(true);

        }
    }
}
