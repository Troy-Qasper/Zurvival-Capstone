using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMusicManager : MonoBehaviour
{
    public AudioSource musicChannel;
    public AudioClip[] inGameMusic;
    public bool isDead;
    PlayerHealth player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerHealth>();
        musicChannel = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(!player.isDead)
        {
            RetrieveInGameMusic();
        }
        else
        {
            musicChannel.Stop();
        }
    }

    void RetrieveInGameMusic()
    {
        AudioClip clip = inGameMusic[UnityEngine.Random.Range(0, inGameMusic.Length)];
        musicChannel.PlayOneShot(clip);
    }
}
