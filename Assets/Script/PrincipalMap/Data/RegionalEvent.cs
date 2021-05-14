using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincipalMap
{
    [CreateAssetMenu(fileName = "RegionEvent", menuName = "Principal/new region event", order = 0)]
    public class RegionalEvent : ScriptableObject
    {
        [TextArea(10,20)][SerializeField] string TextInEvent;
        [TextArea(8, 10)] [SerializeField] string textConfirmButton;
        [TextArea(8,10)] [SerializeField] string textDeclineButton;
        [SerializeField] string textFirstButton;
        [SerializeField] string textSecondButton;
        [SerializeField] Sprite spriteBackGround;
        [SerializeField] bool firstButtonBool = false;
        [SerializeField] bool secondButtonBool = false;
        [SerializeField] TypeRegionEvent typeRegionEvent;
        [SerializeField] int healthTrue = 0;
        [SerializeField] int healthFalse = 0;
        [SerializeField] int coinTrue = 0;
        [SerializeField] int coinFalse = 0;
        [SerializeField] int moveToPosition = 1;

        [Header("Win")]
        [SerializeField] public int winCondition = 100;

        [Header("More events")]
        [SerializeField] public bool workinWithMoreEvents = false;
        [SerializeField] public bool firstButtonMoreEvent = false;
        [SerializeField] public RegionalEvent newEventRegionalToGo;

        public string GetPrincipalText() => TextInEvent;

        public string GetFirstButtonText() => textFirstButton;

        public string GetSecondButtonText() => textSecondButton;

        public string GetConfirmButtonText() => textConfirmButton;

        public string GetDeclineButtonText() => textDeclineButton;

        public bool GetFirstButtonBool() => firstButtonBool;

        public bool GetSecondButtonBool() => secondButtonBool;

        public TypeRegionEvent GetTypeRegionEvent() => typeRegionEvent;

        public Sprite GetSpriteBackGround() => spriteBackGround;

        public int GetHealthTrue() => healthTrue;

        public int GetHealthFalse() => healthFalse;

        public int GetCoinTrue() => coinTrue;

        public int GetCoinFalse() => coinFalse;

        public int GetMovePosition() => moveToPosition;

    }

    public enum TypeRegionEvent {
        Combat,
        Coin,
        Move, 
        Health, 
        FinishGame
    }
}
