using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincipalMap {
    public class ViewControlGameMap : View
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip[] audioClips;

        ControlGameMap controlGameMap;

        private void Start()
        {
            controlGameMap = GetComponent<ControlGameMap>();
        }

        public override void SetNewRandomDiceMove()
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();

            m_lastDiceMoveValue = controlGameMap.GetNewRollMove();
            StartCoroutine(RollDiceMove());
        }

        public override void SetNewMovementPlayer()
        {
            base.SetNewMovementPlayer();



            controlGameMap.SetMovementPlayer(m_lastDiceMoveValue);

        }

    }
}
