using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int initialZombieWaveCount = 5;
    public int initialTitanWaveCount = 2;

    public int titanPointValue = 150;
    public int zombiePointValue = 100;

    public int currentZombieWave;
    public int currentTitanWave;

    public float spawnDelay = 0.5f;

    public int currWave = 0;
    public float waveCooldown = 5.0f;
    public static int waveTracker = 0;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public float rangeValue_1 = -10f;
    public float rangeValue_2 = 10f;
    

    public int pointCount = 0;

    public List<Enemy> currentZombiesAlive;
    public List<Enemy> currentTitansAlive;

    public GameObject zombiePrefab;
    public GameObject titanPrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;

    public TextMeshProUGUI currWaveUI;

    public TextMeshProUGUI pointCountUI;

    public AudioClip inGameMusic;

    public void Start()
    {
        currentZombieWave = initialZombieWaveCount;
        currentTitanWave = initialTitanWaveCount;
        SoundManager.Instance.PlayInGameMusic(SoundManager.Instance.inGameMusic);
        StartNextWave();
    }

    //change
    private void StartNextWave()
    {
        Debug.Log("Starting next wave...");

        currentZombiesAlive.Clear();
        currentTitansAlive.Clear();

        currWave++;

        waveTracker = currWave;
        GlobalRefs.Instance.waveNumber = currWave;
        GlobalRefs.Instance.pointScore = pointCount;

        //insert bonus round checkpoint points here

        currWaveUI.text = "Wave: " + currWave.ToString();
        
        StartCoroutine(SpawnZombieWave());
        if(waveTracker % 5 == 0)
        {
            StartCoroutine(SpawnTitanWave());
        }
    }

    private IEnumerator SpawnZombieWave()
    {
        for(int i = 0; i < currentZombieWave; i++)
        {
            Vector3 spawnOffest = new Vector3(Random.Range(rangeValue_1, rangeValue_2), 0f, Random.Range(rangeValue_1, rangeValue_2));
            Vector3 spawnPos = transform.position + spawnOffest;

            var zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);          

            Enemy enemyZombieScript = zombie.GetComponent<Enemy>();         

            currentZombiesAlive.Add(enemyZombieScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private IEnumerator SpawnTitanWave()
    {
        for (int j = 0; j < currentTitanWave; j++)
        {
            Vector3 spawnOffest = new Vector3(Random.Range(rangeValue_1, rangeValue_2), 0f, Random.Range(rangeValue_1, rangeValue_2));
            Vector3 spawnPos = transform.position + spawnOffest;

            var titan = Instantiate(titanPrefab, spawnPos, Quaternion.identity);

            Enemy enemyTitanScript = titan.GetComponent<Enemy>();

            currentTitansAlive.Add(enemyTitanScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        List<Enemy> zombiesToRemove = new List<Enemy>();
        List<Enemy> titansToRemove = new List<Enemy>();

        foreach(Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
                pointCount += zombiePointValue;
            }
        }
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);           
        }
        zombiesToRemove.Clear();

        foreach (Enemy titan in currentTitansAlive)
        {
            if (titan.isDead)
            {
                titansToRemove.Add(titan);
                pointCount += titanPointValue;
            }
        }
        //gets points and sends to global refs for saving, will change later, is bad
        foreach (Enemy titan in titansToRemove)
        {
            currentTitansAlive.Remove(titan);
        }
        titansToRemove.Clear();
        GlobalRefs.Instance.playerPoints = pointCount;
        pointCountUI.text = "Points: " + pointCount.ToString();

        if (currentZombiesAlive.Count == 0 && currentTitansAlive.Count == 0 && inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }

        if(inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }

        else
        {
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;

        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        waveOverUI.gameObject.SetActive(false);

        currentZombieWave *= 2;
        StartNextWave();
    }
}
