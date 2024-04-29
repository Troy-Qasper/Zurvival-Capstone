using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SaveLoadManager : MonoBehaviour
{
    //singleton
    public static SaveLoadManager Instance { get; set; }

    public GameObject Player { get => player; }

    private GameObject player;

    public string highestRoundAchieved = "BestWaveSavedValue";
    public string highestScoreAchieved = "BestScoreSavedValue";

    //singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool isSavingToJson;

    #region || ------ High Score Section ------ ||
    public void SaveHighestRound(int score)
    {
        PlayerPrefs.SetInt(highestRoundAchieved, score);
        if(score >= 1)
        {
            LevelCompleted.UnlockNewLevel();
        }
    }

    public void SaveHighestScore(int score)
    {
        PlayerPrefs.SetInt(highestScoreAchieved, score);
    }

    public int LoadHighestRounds()
    {
        if(PlayerPrefs.HasKey(highestRoundAchieved))
        {
            return PlayerPrefs.GetInt(highestRoundAchieved);
        }
        else
        {
            return 0;
        }
    }
    public int LoadHighestScore()
    {
        if (PlayerPrefs.HasKey(highestScoreAchieved))
        {
            return PlayerPrefs.GetInt(highestScoreAchieved);
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region || ------- General Section ------- ||

    public void SaveGame()
    {
        AllGameData data = new AllGameData();

        data.playerData = GetPlayerData();

        SaveAllGameData(data);
    }

    private PlayerData GetPlayerData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float playerHealth = GlobalRefs.Instance.playerHealth;
        int currWave = GlobalRefs.Instance.waveNumber;

        float[] playerPosAndRot = new float[6];
        playerPosAndRot[0] = player.transform.position.x;
        playerPosAndRot[1] = player.transform.position.y;
        playerPosAndRot[2] = player.transform.position.z;

        playerPosAndRot[3] = player.transform.rotation.x;
        playerPosAndRot[4] = player.transform.rotation.y;
        playerPosAndRot[5] = player.transform.rotation.z;

        return new PlayerData(currWave, playerHealth, playerPosAndRot);
    }

    public void SaveAllGameData(AllGameData gameData)
    {
        if(isSavingToJson)
        {
            //SaveGameDataToJsonFile();
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }
    #endregion

    #region || ------- To Binary Section ------- ||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save_game.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

        print("data saved to" + Application.persistentDataPath + "/save_game.bin");
    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            return data;
        }
        else
            return null;
    }

    #endregion

    #region || ------- Settings Section ------- ||

    #region || ------- Volume Settings ------- ||
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float sfx;
        public float master;
        public float zombie;
        public float inGameMusic;
    }

    //Saves volume settings using PlayerPrefs
    public void SaveVolumeSettings(float _music, float _sfx, float _master, float _zombie, float _inGameMusic)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            sfx = _sfx,
            master = _master,
            zombie = _zombie,
            inGameMusic = _inGameMusic
        };

        PlayerPrefs.SetString("VolumeSettings", JsonUtility.ToJson(volumeSettings));
        PlayerPrefs.Save();

        print("Saved to Player Pref");
    }

    //Loads VolumeSettings within PlayerPrefs using VolumeSettings set within SettingsManager
    public VolumeSettings LoadVolumeSettings()
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("VolumeSettings"));
    }
    #endregion

    #endregion
}
