using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class PlayerHealth : MonoBehaviour
    {
    private float _health;
        public float Health { get => _health; set => _health = value; }
        private float lerpTimer;
        public float maxHealth = 100f;
        public float chipSpeed = 2f;
        public Image frontHealthBar;
        public Image backHealthBar;
        public GameObject bloodyScreen;
    public int level;

        public bool isDead;

        public GameObject gameOverUI;
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

        Health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
        //level = SpawnController.Instance.currWave;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            UpdateHealthUI();
            /* if (Input.GetKeyDown(KeyCode.A))
             {
                 TakeDamage(Random.Range(5, 10));
             }
             if (Input.GetKeyDown(KeyCode.S))
             {
                 RestoreHealth(Random.Range(5, 10));
             }*/
        }

        public void UpdateHealthUI()
        {
            float fillF = frontHealthBar.fillAmount;
            float fillB = backHealthBar.fillAmount;
            float hFraction = Health / maxHealth;
            if (fillB > hFraction)
            {
                frontHealthBar.fillAmount = hFraction;
                backHealthBar.color = Color.red;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
            }
            if (fillF < hFraction)
            {
                backHealthBar.color = Color.green;
                backHealthBar.fillAmount = hFraction;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
            }
        }

    //receives and applies damage taken from enemy units and plays appropriate sounds for damage/death
        public void TakeDamageFromEnemy(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.playerDeathSound);
            SoundManager.Instance.zombieChannel.Pause();
                SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
                SoundManager.Instance.playerChannel.PlayDelayed(2f);
                print("Player Dead");
                PlayerDeath();
                isDead = true;
                //Game over
            }
            else
            {
            GlobalRefs.Instance.playerHealth = Health;
                print("Player Hit");
                StartCoroutine(BloodyScreenEffect());
            SoundManager.Instance.PlaySFX(SoundManager.Instance.playerHurtSound);
        }
            lerpTimer = 0f;
        }

    //if a players health reaches 0 this method is initiated disabling movement and camera functionality. starting a coroutine to run the ShowGameOverUI
        private void PlayerDeath()
        {
            GetComponent<PlayerMotor>().enabled = false;
            GetComponent<PlayerLook>().enabled = false;
            GetComponent<InputManager>().enabled = false;

            GetComponentInChildren<Animator>().enabled = true;

            GetComponent<BlackOut>().StartFade();

            StartCoroutine(ShowGameOverUI());
    }
    //saves highest round and score achieved during game (will be map specific soon) and then returns player to the main menu
        private IEnumerator ShowGameOverUI()
        {
            yield return new WaitForSeconds(1f);

            gameOverUI.gameObject.SetActive(true);

            int waveSurvived = GlobalRefs.Instance.waveNumber;
            int pointsScored = GlobalRefs.Instance.pointScore;
        if (waveSurvived == 0)
        {
            //SaveLoadManager.Instance.SaveHighestRound(0);
            SaveLoadManager.Instance.SaveHighestScore(0);
        }
        if (waveSurvived - 1 > SaveLoadManager.Instance.LoadHighestRounds() && waveSurvived > 0)
            {
                SaveLoadManager.Instance.SaveHighestRound(waveSurvived - 1);
            }
        if (pointsScored > SaveLoadManager.Instance.LoadHighestScore() && pointsScored > 0)
        {
            SaveLoadManager.Instance.SaveHighestScore(pointsScored);
        }

            StartCoroutine(ReturnToMainMenu());
    }

        private IEnumerator ReturnToMainMenu()
        {
            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("MainMenu");
    }
    //restore health ready to be implemented with items, just haven't had time to place interactable items in world.
        public void RestoreHealth(float healthRestored)
        {
            Health += healthRestored;
            lerpTimer = 0;
        }

        private IEnumerator BloodyScreenEffect()
        {
            if (bloodyScreen.activeInHierarchy == false)
            {
                bloodyScreen.SetActive(true);
            }
            var image = bloodyScreen.GetComponentInChildren<Image>();
            Color startColor = image.color;
            startColor.a = 1f;
            image.color = startColor;

            float duration = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

                Color newColor = image.color;
                newColor.a = alpha;
                image.color = newColor;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            if (bloodyScreen.activeInHierarchy)
            {
                bloodyScreen.SetActive(false);
            }
        }

    //if hit by a zombie hand (sphere mesh with collider) then take damage from the enemy.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Zombie Hand"))
            {
                if (isDead == false)
                {
                    TakeDamageFromEnemy(other.gameObject.GetComponent<ZombieHand>().damage);
                }
            }
        }
}

