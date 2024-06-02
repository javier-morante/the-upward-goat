using System.Collections.Generic;
using System.Linq;
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

    // Singleton instance of the DataPersistanceManager
    public static DataPersistanceManager instance { get; private set; }

    void Awake()
    {
        // Singleton pattern implementation
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        // Initialize file data handlers
        this.fileDataHandlerD = new FileDataHandler<GameData>(Application.persistentDataPath, fileNameGameData);
        this.fileDataHandlerS = new FileDataHandler<SettingsData>(Application.persistentDataPath, fileNameSettings);
    }

    void OnEnable()
    {
        // Register scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unregister scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Event handler for scene loaded event
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find all data persistence objects in the scene
        this.gameDataPersistances = FindAllDataPersistance<IDataPersistence<GameData>>();
        this.settingsPersistances = FindAllDataPersistance<IDataPersistence<SettingsData>>();
        // Load game data and settings
        LoadGameData();
        LoadSettings();
    }

    // Find all objects implementing IDataPersistence<T>
    private List<T> FindAllDataPersistance<T>()
    {
        IEnumerable<T> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<T>();

        return new List<T>(dataPersistencesObjects);
    }

    // Start a new game
    public void NewGame()
    {
        this.gameData = new GameData();
        SaveGameData();
    }

    // Load game data
    void LoadGameData()
    {
        this.gameData = this.fileDataHandlerD.Load();

        if (this.gameData == null)
        {
            Debug.LogWarning("No game data was found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<GameData> dataPersistenceObj in gameDataPersistances)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    // Save game data
    public void SaveGameData()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No game data found. A new game needs to be started");
            return;
        }
        if (this.gameDataPersistances == null)
        {
            Debug.LogWarning("No data persistences found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<GameData> dataPersistenceObj in gameDataPersistances)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        fileDataHandlerD.Save(gameData);
    }

    // Load settings data
    void LoadSettings()
    {
        this.settingsData = this.fileDataHandlerS.Load();

        if (this.settingsData == null)
        {
            Debug.LogWarning("No settings data was found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<SettingsData> dataPersistenceObj in settingsPersistances)
        {
            dataPersistenceObj.LoadData(settingsData);
        }
    }

    // Save settings data
    public void SaveSettings()
    {
        if (this.settingsData == null)
        {
            Debug.LogWarning("No settings data found. A new game needs to be started");
            settingsData = new SettingsData();
        }
        if (this.settingsPersistances == null)
        {
            Debug.LogWarning("No settings persistences found. A new game needs to be started");
            return;
        }

        foreach (IDataPersistence<SettingsData> dataPersistenceObj in settingsPersistances)
        {
            dataPersistenceObj.SaveData(ref settingsData);
        }

        fileDataHandlerS.Save(settingsData);
    }

    // Save game data and settings on application quit
    void OnApplicationQuit()
    {
        SaveGameData();
        SaveSettings();
    }

    // Get game data
    public GameData GetGameData()
    {
        return gameData;
    }

    // Check if game data exists
    public bool HasGameData()
    {
        return gameData != null;
    }

    // Delete game data
    public void DeleteGameData()
    {
        this.gameData = null;
        this.fileDataHandlerD.DeleteFile();
    }
}
