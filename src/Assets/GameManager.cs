using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameManager : MonoBehaviour
{
    public int waveNumber;

    public int pointScore;

    public static int playerPoints;
    public Vector3 playerPosition;
    public PlayerData playerData;
    PlayerHealth player;

    public GameManager()
    {
        playerPoints = 0;
        player = new PlayerHealth();
        playerPosition = Vector3.zero;
        //playerData = new PlayerData(player);
    }
}
