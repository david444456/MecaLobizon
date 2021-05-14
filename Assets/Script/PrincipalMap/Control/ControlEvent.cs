using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincipalMap {
    public class ControlEvent : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] public GameObject UIEventShow;
        [SerializeField] GameObject firstButtonGO;
        [SerializeField] GameObject secondButtonGO;
        [SerializeField] GameObject finishEventButtonGO;
        [SerializeField] public Text textUIEventShow;
        [SerializeField] public Image imageBackGroundEvent;
        [SerializeField] public Text firstButtontextUIEventShow;
        [SerializeField] public Text secondButtontextUIEventShow;
        [SerializeField] float timeWaitFinishEvent = 1;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip[] audioClips;

        [Header("Win text")]
        [SerializeField] [TextArea(10,20)] string textWin;

        [Header("Group event")]
        [SerializeField] GroupRegionalEvent[] groupRegionalEvents;

        RegionalEvent actualRegionalEvent;
        ControlGameMap controlGameMap;

        bool nextEventIsCombat = false;
        bool alreadySeeWinCondiciton = false;

        void Start()
        {
            controlGameMap = GetComponent<ControlGameMap>();
        }

        public void ActiveNewEvent(int index)
        {

            actualRegionalEvent = groupRegionalEvents[index].GetRandomRegionalEvent();

            //verificated win condition
            if (actualRegionalEvent.GetTypeRegionEvent() == TypeRegionEvent.FinishGame)
            {
                if (controlGameMap.actualCoin > actualRegionalEvent.winCondition)
                {
                    activeNewEventControlUI();
                    textUIEventShow.text = textWin;
                }
                else
                {
                    if (!alreadySeeWinCondiciton)
                    {
                        activeNewEventControlUI();
                        alreadySeeWinCondiciton = true;
                    }
                    else {
                        FinishEventButton();
                    }
                    firstButtonGO.SetActive(false);
                }
            }
            else {

                activeNewEventControlUI();
            }
        }

        private void activeNewEventControlUI()
        {
            UIEventShow.SetActive(true);
            firstButtonGO.SetActive(true);
            audioSource.clip = audioClips[ Random.Range(0, audioClips.Length)];
            audioSource.Play();

            textUIEventShow.text = actualRegionalEvent.GetPrincipalText();
            imageBackGroundEvent.sprite = actualRegionalEvent.GetSpriteBackGround();
            firstButtontextUIEventShow.text = actualRegionalEvent.GetFirstButtonText();
            secondButtontextUIEventShow.text = actualRegionalEvent.GetSecondButtonText();
        }

        public void ConfirmEvent() {
            //more events
            if (actualRegionalEvent.workinWithMoreEvents) {
                if (actualRegionalEvent.firstButtonMoreEvent) {
                    actualRegionalEvent = actualRegionalEvent.newEventRegionalToGo;
                    activeNewEventControlUI();
                    return;
                }
            }

            //no more events
            DecideTypeEvent(actualRegionalEvent.GetFirstButtonBool());
            print("confirmo: " + actualRegionalEvent.GetPrincipalText());
            textUIEventShow.text = actualRegionalEvent.GetConfirmButtonText();
        }

        public void RefuseEvent() {
            //more events
            if (actualRegionalEvent.workinWithMoreEvents)
            {
                if (!actualRegionalEvent.firstButtonMoreEvent)
                {
                    actualRegionalEvent = actualRegionalEvent.newEventRegionalToGo;
                    activeNewEventControlUI();
                    return;
                }
            }

            //no more events
            DecideTypeEvent(actualRegionalEvent.GetSecondButtonBool());
            print("Me rehuso" +actualRegionalEvent.GetPrincipalText());
            textUIEventShow.text = actualRegionalEvent.GetDeclineButtonText();
        }

        public void FinishEventButton() {
            if(nextEventIsCombat) controlGameMap.EventCombat(actualRegionalEvent.progressionCombatLevel, actualRegionalEvent.characterEnemy);
            StartCoroutine(FinishEvent());
        }

        private void DecideTypeEvent(bool confirm) {
            desactiveButtonFirstStageEvent();

            switch (actualRegionalEvent.GetTypeRegionEvent()) {
                case TypeRegionEvent.Coin:
                    if (confirm) controlGameMap.EventCoin(actualRegionalEvent.GetCoinTrue());
                    else controlGameMap.EventCoin(actualRegionalEvent.GetCoinFalse());
                    break;
                case TypeRegionEvent.Move:
                    if (confirm) {
                        controlGameMap.EventSubtractHalfCoin();
                        controlGameMap.EventMoveNewPosition();

                    }
                    break;
                case TypeRegionEvent.Combat:
                    if (confirm) nextEventIsCombat = true;
                    break;
                case TypeRegionEvent.Health:
                    if (confirm) controlGameMap.EventHealth(actualRegionalEvent.GetHealthTrue());
                    else controlGameMap.EventHealth(actualRegionalEvent.GetHealthFalse());
                    break;
                case TypeRegionEvent.FinishGame:
                    if (controlGameMap.actualCoin > actualRegionalEvent.winCondition) controlGameMap.EventFinishGame();
                    break;
            }
        }

        private void desactiveButtonFirstStageEvent() {
            firstButtonGO.SetActive(false);
            secondButtonGO.SetActive(false);
            finishEventButtonGO.SetActive(true);
        }

        IEnumerator FinishEvent() {
            controlGameMap.FinishEvent();
            yield return new WaitForSeconds(timeWaitFinishEvent);

        }
    }
}
