using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int currWave;
    public float health;
    public float[] playerPositionAndRotation;

    public PlayerData(PlayerHealth player)
    {
        currWave = GlobalRefs.Instance.waveNumber;
        health = player.Health;

        playerPositionAndRotation = new float[3];

        playerPositionAndRotation[0] = player.transform.position.x;
        playerPositionAndRotation[1] = player.transform.position.y;
        playerPositionAndRotation[2] = player.transform.position.z;
    }
    public PlayerData(int _currWave, float _health, float[] _playerPosAndRot)
    {
        currWave = _currWave;
        health = _health;
        playerPositionAndRotation = _playerPosAndRot;
    }
}
