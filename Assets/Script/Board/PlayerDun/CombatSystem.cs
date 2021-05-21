using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static Board.ProgressionCombat;

namespace Board
{
    public class CombatSystem : MonoBehaviour, ICombatSystem
    {
        [SerializeField] ProgressionCombat progressionCombat;
        [SerializeField] CharacterBoard characterBoard;
        [SerializeField] GameObject instantiateCombatZone;
        [SerializeField] float correctionXEnemy = .44f;

        [Header("Initial Combat")]
        [SerializeField] GameObject transformCameraCombat;
        [SerializeField] GameObject transformCameraMain;
        [SerializeField] PlayableDirector playableDirector;

        [Header("UI Combat")]
        [SerializeField] GameObject GOUICombatSystem;
        [SerializeField] Slider sliderHealthPlayer;
        [SerializeField] Slider sliderHealthEnemy;

        //enemy
        int actualHealthEnemy = 0;
        int levelEnemy = 0;
        float attackSpeedEnemy = 0;

        //enemy
        float attackSpeedPlayer = 0;

        //cmbat
        float timeLastAttackEnemy = 0;
        float timeLastAttackPlayer = 0;
        int lastAttackDamageEnemy = 0;
        int lastAttackDamagePlayer = 0;

        bool isCombat = false;

        //ingame
        Animator animatorEnemy;
        GameObject gameObjectCombatZone;
        GameObject enemy;

        private void Start()
        {
            levelEnemy = characterBoard.GetInitialLevel();
        }

        private void Update()
        {
            if (!isCombat) return;

            timeLastAttackEnemy += Time.deltaTime;
            timeLastAttackPlayer += Time.deltaTime;

            if (timeLastAttackEnemy >= attackSpeedEnemy) {
                StartCoroutine(AttackEnemy());
                timeLastAttackEnemy = 0;
            } else if (timeLastAttackPlayer >= attackSpeedPlayer) {
                AttackPlayerToEnemy();
                timeLastAttackPlayer = 0;
            }

        }

        public void NewCombatTypeCharacter(Vector2 lastPosition, GameObject prefabEnemy, CharacterBoard characterBoardEnemy)
        {
            //instanciar combate en la zona que llega por referencia
            gameObjectCombatZone = Instantiate(instantiateCombatZone, new Vector3(lastPosition.x, 0, lastPosition.y), MediatorBoard.Mediator.GetRotationPlayer());

            //cambiar la camara, animacion para mostrar la zona de combate
            GameObject goCamera = GameObject.Find("CameraCombatZone");
            transformCameraCombat.transform.position = goCamera.transform.position;
            transformCameraCombat.transform.rotation = goCamera.transform.rotation;
            transformCameraCombat.SetActive(true);
            transformCameraMain.gameObject.SetActive(false);
            playableDirector.Play();

            //para usar con el player
            goCamera.transform.localPosition = goCamera.transform.localPosition + new Vector3(correctionXEnemy, -0.7f, 0.91f);

            //instanciar el enemigo en la zona 
            characterBoard = characterBoardEnemy;
            enemy = prefabEnemy;//Instantiate(characterBoard.prefabGameObject, goCamera.transform.position, MediatorBoard.Mediator.GetRotationPlayer());
            enemy.transform.position = goCamera.transform.position;
            enemy.transform.rotation = MediatorBoard.Mediator.GetRotationPlayer();

            //progcombat
            progressionCombat = MediatorBoard.Mediator.GetProgressionCombat();

            //mover mi jugador a la zona, 
            //MediatorBoard.Mediator.ChangePositionPlayerToCombat(new Vector3(lastPosition.x - correctionXEnemy, 0.65f, lastPosition.y));

            //actualizar  UI, tiempo, vida de enemigo 
            animatorEnemy = enemy.GetComponent<Animator>();
            StartDataEnemy((int)progressionCombat.GetStat(Stat.Health, characterBoard.GetCharacterClass(), levelEnemy),
                                progressionCombat.GetStat(Stat.AttackSpeed, characterBoard.GetCharacterClass(), levelEnemy));

            ///update life player
            //actualizar habilidades que puede tirar mi jugador, ui combat player
            StartDataCombatPlayer();

            //empieza a correr el tiempo, empiezan a recargarse las habilidades
            isCombat = true;

            //empezar loop de combate
            GOUICombatSystem.SetActive(true);

            //update:
            ////enemigo tiene que mirar si puede atacar en el update (este), entonces empieza a atacar

        }

        public void AttackPlayerToEnemy() {
            MediatorBoard.Mediator.SetAttackPlayerAnimation();
            lastAttackDamagePlayer = MediatorBoard.Mediator.GetDamageAttackPlayer();
            
        }

        public void TakeDamageEnemy()
        {
            if (animatorEnemy == null) return;
            actualHealthEnemy -= lastAttackDamagePlayer;
            animatorEnemy.SetTrigger("Hit");

            if (actualHealthEnemy <= 0)
            {
                animatorEnemy.SetTrigger("Die");
                FinishCombat();
                print("Terminar combate");
            }

            UpdateSliderHealthEnemy();
        }

        public void TakeDamagePlayer()
        {
            MediatorBoard.Mediator.SetNewHealth(lastAttackDamageEnemy);
            int actualHeatlhPlayer = MediatorBoard.Mediator.GetHealth();

            if (actualHeatlhPlayer <= 0)
            {
                FinishCombat();
                print("Terminar combate, perdiste");
            }

            UpdateSliderHealthPlayer(actualHeatlhPlayer);
        }

        public bool GetActualStateCombat() => isCombat;

        private void StartDataEnemy(int maxHealth, float Atspeed)
        {
            print(maxHealth + " " + characterBoard.GetInitialLevel());
            actualHealthEnemy = maxHealth;
            sliderHealthEnemy.maxValue = maxHealth;
            attackSpeedEnemy = Atspeed;
            UpdateSliderHealthEnemy();
        }

        private void StartDataCombatPlayer() {
            attackSpeedPlayer = MediatorBoard.Mediator.GetAttackSpeedPlayer();

            UpdateSliderHealthPlayer(MediatorBoard.Mediator.GetHealth());
        }

        private void UpdateSliderHealthEnemy() => sliderHealthEnemy.value = actualHealthEnemy;

        private void UpdateSliderHealthPlayer(int health) => sliderHealthPlayer.value = health;

        private void FinishCombat() {

            StartCoroutine(FinishTheGameWithTime());
            GOUICombatSystem.SetActive(false);
            isCombat = false;

        }

        IEnumerator AttackEnemy()
        {
            yield return new WaitForSeconds(1);
            animatorEnemy.SetTrigger("Attack");
            lastAttackDamageEnemy = (int) progressionCombat.GetStat(Stat.Damage, characterBoard.GetCharacterClass(), characterBoard.GetInitialLevel());

            yield return new WaitForSeconds(1);
            //StartAttackPlayer();
        }

        IEnumerator FinishTheGameWithTime()
        {
            if (isCombat)
            {

                yield return new WaitForSeconds(1);

                transformCameraCombat.SetActive(false);
                transformCameraMain.SetActive(true);

                yield return new WaitForSeconds(1);

                Destroy(gameObjectCombatZone);
                Destroy(enemy, 2);
                levelEnemy++;
                MediatorBoard.Mediator.CompleteEventPlayer();
                MediatorBoard.Mediator.SetCoinRewardEvent((int)progressionCombat.GetStat(Stat.RewardCoin, characterBoard.GetCharacterClass(), levelEnemy));
            }
        }

    }
}
