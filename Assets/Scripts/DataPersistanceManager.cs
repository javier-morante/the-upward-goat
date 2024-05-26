using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistences;
    private FileDataHandler fileDataHandler;

    public static DataPersistanceManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {

            Debug.LogError("More than on Data persistance");

        }
        instance = this;
    }

    void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        this.dataPersistences = FindAllDataPersistance();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistance()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObjects);
    }

    void NewGame()
    {
        this.gameData = new GameData();
    }

    void LoadGame()
    {

        this.gameData = this.fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistences)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        
        Debug.Log("Loaded data jump: "+gameData.jumpCount);

    }

    void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistences)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved data vector: "+gameData.playerPosition);
        fileDataHandler.Save(gameData);
    }

    void OnApplicationQuit(){
        SaveGame();
    }
}
