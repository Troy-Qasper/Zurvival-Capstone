using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private PlayerData playerData;
    public static DataPersistenceManager Instance { get; set; }
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    public void NewGame()
    {
        
    }
    public void LoadGame()
    {

    }
    public void SaveGame()
    {

    }
}
