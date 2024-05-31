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

    [SerializeField] private string fileNameGameData;
    [SerializeField] private string fileNameSettings;

    private GameData gameData;
    private SettingsData settingsData;

    private List<IDataPersistence<GameData>> gameDataPersistances;
    private List<IDataPersistence<SettingsData>> settingsPersistances;
    private FileDataHandler<GameData> fileDataHandlerD;
    private FileDataHandler<SettingsData> fileDataHandlerS;


    public static DataPersistanceManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.fileDataHandlerD = new FileDataHandler<GameData>(Application.persistentDataPath,fileNameGameData);
        this.fileDataHandlerS = new FileDataHandler<SettingsData>(Application.persistentDataPath,fileNameSettings);
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        this.gameDataPersistances = FindAllDataPersistance<IDataPersistence<GameData>>();
        this.settingsPersistances = FindAllDataPersistance<IDataPersistence<SettingsData>>();
        LoadGameData();
        LoadSettings();
    }

    private List<T> FindAllDataPersistance<T>()
    {
        IEnumerable<T> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<T>();

        return new List<T>(dataPersistencesObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    void LoadGameData()
    {
        this.gameData = this.fileDataHandlerD.Load();

        if (this.gameData == null)
        {
            Debug.LogWarning("No was found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<GameData> dataPersistenceObj in gameDataPersistances)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGameData()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No was found data.A new game needs to be started");
            return;
        }
        if (this.gameDataPersistances == null)
        {
            Debug.LogWarning("No was found datapersistances.A new game needs to be started");
            return;
        }
        
        foreach (IDataPersistence<GameData> dataPersistenceObj in gameDataPersistances)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        
        fileDataHandlerD.Save(gameData);
    }

    void LoadSettings()
    {
        this.settingsData = this.fileDataHandlerS.Load();

        if (this.settingsData == null)
        {
            Debug.LogWarning("No was found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<SettingsData> dataPersistenceObj in settingsPersistances)
        {
            dataPersistenceObj.LoadData(settingsData);
        }
    }

    public void SaveSettings()
    {
        if (this.settingsData == null)
        {
            Debug.LogWarning("No was found  settings.A new game needs to be started");
            settingsData = new SettingsData();
        }
        if (this.settingsPersistances == null)
        {
            Debug.LogWarning("No was found settingsPersistences.A new game needs to be started");
            return;
        }
        
        foreach (IDataPersistence<SettingsData> dataPersistenceObj in settingsPersistances)
        {
            dataPersistenceObj.SaveData(ref settingsData);
        }
        
        fileDataHandlerS.Save(settingsData);
    }

    void OnApplicationQuit(){
        SaveGameData();
        SaveSettings();
    }

    public GameData GetGameData(){
        return gameData;
    }

    public bool HasGameData(){
        return gameData != null;
    } 

    public void DeleteGameData(){
        this.gameData = null;
        this.fileDataHandlerD.DeleteFile();
    }
}
