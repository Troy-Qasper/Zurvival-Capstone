using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//singleton
public class GlobalRefs : MonoBehaviour
{
    public static GlobalRefs Instance { get; set; }

    public GameObject bulletImpactEffect;

    public GameObject grenadeExplosionEffect;
    public GameObject smokeGrenadeEffect;

    public GameObject zombieBloodSpray;

    public int waveNumber;

    public int pointScore;

    public int playerPoints;

    public float playerHealth;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
