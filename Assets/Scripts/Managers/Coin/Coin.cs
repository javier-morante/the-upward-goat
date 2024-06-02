using UnityEngine;

public class Coin : Subject<PlayerEvents>, IDataPersistence<GameData>{

    // Unique identifier for the coin
    public string id;
    // Flag indicating whether the coin has been collected
    private bool collected;
    // Audio clip for the coin
    [SerializeField] private AudioClip coin;

    // Generate a unique identifier for the coin
    [ContextMenu("Generate id")]
    private void GenerateGui(){
        id = System.Guid.NewGuid().ToString();
    }

    void Start(){
        // Find the UIManager object and register it as an observer
        UiManager UiManager = GameObject.Find("GameManager").GetComponent<UiManager>();
        this.AddObserver(UiManager);
    }

    // Handle collision with other 2D collider objects
    void OnTriggerEnter2D(Collider2D collider2D){
        // Check if the collider belongs to the player
        if(collider2D.CompareTag("Player")){
            // Notify observers that the coin has been collected
            this.NotifyObservers(PlayerEvents.CoinCollected);
            // Play the coin collection sound
            AudioManager.instance.PlaySoundFX(coin, transform, 1f);
            // Deactivate the coin object
            gameObject.SetActive(false);
            // Set the collected flag to true
            collected = true;
        }
    }

    // Load coin data from game data
    public void LoadData(GameData data)
    {
        // Try to retrieve the collected status of the coin using its id
        data.coins.TryGetValue(id,out collected);
        // If the coin has been collected, deactivate the coin object
        if(collected){
            this.gameObject.SetActive(false);
        }
    }

    // Save coin data to game data
    public void SaveData(ref GameData data)
    {
        // If the coin's id already exists in the coins dictionary, remove it
        if (data.coins.ContainsKey(id))
        {
            data.coins.Remove(id);
        }
        // Add or update the collected status of the coin in the coins dictionary
        data.coins.Add(id,collected);
    }
}
