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

        [Header("UI")]
        [SerializeField] Text textTimeCombat;
        [SerializeField] GameObject gameObjectAbilitiesPlayer;
        [SerializeField] Slider[] sliderAbilitiesPlayer;
        [SerializeField] Image[] imageBackGroundAbilitiesPlayer;
        [SerializeField] Image[] imageFillAbilitiesPlayer;
        [SerializeField] Text[] textValueEnergyPlayer;
        [SerializeField] Text[] textValueDamagePlayer;

        [Header("UI Combat")]
        [SerializeField] GameObject gameObjectControlSliders;
        [SerializeField] Slider sliderHealthPlayer;
        [SerializeField] Slider sliderHealthEnemy;


        AbilitiesCharacter[] abilitiesPlayer = new AbilitiesCharacter[3];
        AbilitiesCharacter[] abilitiesEnemy;
        int[] timeAbilitiesEnemy = new int[5];

        //enemy
        int lastIndexAbilitiesEnemy = 0;
        int actualHealthEnemy = 0;
        int levelEnemy = 0;

        float actualEnergy = 0;
        float actualEnergyEnemy = 0;
        bool isCombat = false;

        //ingame
        Animator animatorEnemy;
        GameObject gameObjectCombatZone;
        GameObject enemy;

        private void Start()
        {
            levelEnemy = characterBoard.GetInitialLevel();
        }

        private void StartTurnPlayer()
        {
            if (!isCombat) return;

            gameObjectControlSliders.SetActive(true);

            actualEnergy = Random.Range(25,40);
            if (actualEnergy >= 1) {
                textTimeCombat.text = actualEnergy.ToString();
                UpdateSlidersAbilitiesPlayer();
            }
        }

        public void FinishTurnPlayer() {
            if (!isCombat) return;

            gameObjectControlSliders.SetActive(false);
            UpdateAbilitiesEnemy();
            print("Finish turn");
        }

        public void AttackPlayerByIndex(int index) {
            if (sliderAbilitiesPlayer[index].maxValue <= actualEnergy)
            {
                MediatorBoard.Mediator.SetAttackPlayerAnimation();
                print("Attack to enemy: " + abilitiesPlayer[index].damage);
                TakeDamageEnemy(abilitiesPlayer[index].damage);

                actualEnergy -= sliderAbilitiesPlayer[index].maxValue;
                textTimeCombat.text = actualEnergy.ToString();

                UpdateSlidersAbilitiesPlayer();
                /*for (int i = 0; i < sliderAbilitiesPlayer.Length; i++)
                {
                    sliderAbilitiesPlayer[i].value = 0;
                }*/
            }
        }

        public void NewCombatTypeCharacter(Vector2 lastPosition)
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
            enemy = Instantiate(characterBoard.prefabGameObject, goCamera.transform.position, MediatorBoard.Mediator.GetRotationPlayer());


            //mover mi jugador a la zona, 
            MediatorBoard.Mediator.ChangePositionPlayerToCombat(new Vector3(lastPosition.x - correctionXEnemy, 0.65f, lastPosition.y));

            //actualizar  UI, tiempo, vida de enemigo 
            animatorEnemy = enemy.GetComponent<Animator>();
            StartDataEnemy((int)progressionCombat.GetStat(Stat.Health, characterBoard.GetCharacterClass(), levelEnemy));



            //update abilities
            List<AbilitiesCharacter> abilitiesCharacters = MediatorBoard.Mediator.GetAbilitiesPlayer();
            for (int i = 0; i< abilitiesPlayer.Length; i++) {
                int indexRandom = Random.Range(0, abilitiesCharacters.Count) ;
                abilitiesPlayer[i] = abilitiesCharacters[indexRandom];
                abilitiesCharacters.RemoveAt(indexRandom);
            }

            abilitiesEnemy = progressionCombat.GetAbilitiesCharacters(characterBoard.GetCharacterClass());

            ///update life player

            //actualizar habilidades que puede tirar mi jugador, ui combat player
            gameObjectAbilitiesPlayer.SetActive(true);
            StartDataUIAbilitiesPlayer();

            //empieza a correr el tiempo, empiezan a recargarse las habilidades
            isCombat = true;

            //empezar loop de combate
            SetRandomAbilitiesEnemyToNextAttack();

            StartTurnPlayer();

            //update:
            ////enemigo tiene que mirar si puede atacar en el update (este), entonces empieza a atacar asi a lo neonazi,
            ////primero elije una habilidad de las que tiene y ahi espera hasta que se pueda tirar, y asi

            ////llenar habilidades player
        }

        //private void UpdateTextTimeCombat() => textTimeCombat.text = _timeCombat.ToString();

        private void SetRandomAbilitiesEnemyToNextAttack() => lastIndexAbilitiesEnemy = Random.Range(0, timeAbilitiesEnemy.Length);

        private void StartDataUIAbilitiesPlayer() {
            sliderHealthPlayer.maxValue = MediatorBoard.Mediator.GetMaxHealth();
            UpdateSliderHealthEnemy();

            for (int i = 0; i < sliderAbilitiesPlayer.Length; i++) {
                sliderAbilitiesPlayer[i].maxValue = abilitiesPlayer[i].timeToAttack;
                textValueDamagePlayer[i].text = abilitiesPlayer[i].damage.ToString();
                textValueEnergyPlayer[i].text = abilitiesPlayer[i].timeToAttack.ToString();
                imageBackGroundAbilitiesPlayer[i].sprite = abilitiesPlayer[i].spriteAbilities;
                imageFillAbilitiesPlayer[i].sprite = abilitiesPlayer[i].spriteAbilities;
                sliderAbilitiesPlayer[i].value = 0;
            }
        }

        public void StartDataEnemy(int maxHealth)
        {
            print(maxHealth + " " + characterBoard.GetInitialLevel());
            actualHealthEnemy = maxHealth;
            sliderHealthEnemy.maxValue = maxHealth;
            UpdateSliderHealthEnemy();
        }

        private void TakeDamageEnemy(int damage) {
            if (animatorEnemy == null) return;
            actualHealthEnemy -= damage;
            animatorEnemy.SetTrigger("Hit");

            if (actualHealthEnemy <= 0) {
                animatorEnemy.SetTrigger("Die");
                FinishCombat();
                print("Terminar combate");
            }

            UpdateSliderHealthEnemy();
        }

        private void TakeDamagePlayer(int damage)
        {
            MediatorBoard.Mediator.SetNewHealth( damage);

            int actualHeatlhPlayer = MediatorBoard.Mediator.GetHealth();

            if (actualHeatlhPlayer <= 0)
            {
                FinishCombat();
                print("Terminar combate");
            }

            UpdateSliderHealthPlayer(actualHeatlhPlayer);
        }

        private void UpdateSliderHealthEnemy() => sliderHealthEnemy.value = actualHealthEnemy;

        private void UpdateSliderHealthPlayer(int health) => sliderHealthPlayer.value = health;

        private void UpdateSlidersAbilitiesPlayer()
        {
            for (int i = 0; i < sliderAbilitiesPlayer.Length; i++)
            {
                if (sliderAbilitiesPlayer[i].maxValue <= actualEnergy)
                    sliderAbilitiesPlayer[i].value = sliderAbilitiesPlayer[i].maxValue;
                else {
                    sliderAbilitiesPlayer[i].value = 0;
                }
            }
        }

        private void UpdateAbilitiesEnemy()
        {
            actualEnergyEnemy = 30;

            //enemy attack?
            if (timeAbilitiesEnemy[lastIndexAbilitiesEnemy] <= actualEnergyEnemy)
            {
                for (int i = 0; i < timeAbilitiesEnemy.Length; i++)
                {
                    timeAbilitiesEnemy[i] = 0;
                }

                StartCoroutine( AttackEnemy(lastIndexAbilitiesEnemy));
                SetRandomAbilitiesEnemyToNextAttack();
            }

        }

        private void FinishCombat() {

            StartCoroutine(FinishTheGameWithTime());
            gameObjectAbilitiesPlayer.SetActive(false);
            isCombat = false;

        }

        IEnumerator AttackEnemy(int indexAb)
        {
            yield return new WaitForSeconds(1);
            animatorEnemy.SetTrigger("Attack");
            TakeDamagePlayer(abilitiesEnemy[indexAb].damage);

            yield return new WaitForSeconds(1);
            StartTurnPlayer();
        }

        IEnumerator FinishTheGameWithTime()
        {
            if (isCombat)
            {

                yield return new WaitForSeconds(1);

                transformCameraCombat.SetActive(false);
                transformCameraMain.SetActive(true);

                yield return new WaitForSeconds(1);

                gameObjectAbilitiesPlayer.SetActive(false);
                Destroy(gameObjectCombatZone);
                Destroy(enemy, 2);
                levelEnemy++;
                MediatorBoard.Mediator.CompleteEventPlayer();
                MediatorBoard.Mediator.SetCoinRewardEvent((int)progressionCombat.GetStat(Stat.RewardCoin, characterBoard.GetCharacterClass(), levelEnemy));
            }
        }
    }
}
