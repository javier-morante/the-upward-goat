using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistences;
    private FileDataHandler<GameData> fileDataHandler;

    public static DataPersistanceManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {

            Debug.LogError("More than on Data persistance");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.fileDataHandler = new FileDataHandler<GameData>(Application.persistentDataPath,fileName);
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("Loaded");
        this.dataPersistences = FindAllDataPersistance();
        LoadGame();
    }

    void OnSceneUnloaded(Scene scene){
        Debug.Log("unloaded");
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistance()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    void LoadGame()
    {

        this.gameData = this.fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No was found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistences)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No was found.A new game needs to be started");
            return;
        }
        
        foreach (IDataPersistence dataPersistenceObj in dataPersistences)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        
        fileDataHandler.Save(gameData);
    }

    void OnApplicationQuit(){
        SaveGame();
    }

    public GameData GetGameData(){
        return gameData;
    }

    public bool HasGameData(){
        return gameData != null;
    } 
}
